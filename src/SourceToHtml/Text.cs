using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weigelt.SourceToHtml
{
	/// <summary>
	/// Provides simplified access to a text.
	/// </summary>
	internal class Text
	{
		private readonly string _Text;
		private int _StartIndex;
		private int _CurrentIndex;

		/// <summary>
		/// Initializes a new instance of the <see cref="Text"/> class.
		/// </summary>
		/// <param name="text">The text to be used by this instance.</param>
		public Text(string text)
		{
			if (text == null)
				throw new ArgumentNullException(nameof(text));
			_Text = text;
			_StartIndex = 0;
			_CurrentIndex = 0;
		}

		/// <summary>
		/// Gets a value indicating whether the end of the text has been reached
		/// by one of the methods for advancing the current position.
		/// </summary>
		/// <value>
		///   <c>true</c> if the end was reached; otherwise, <c>false</c>.
		/// </value>
		public bool EndReached { get; private set; }

		/// <summary>
		/// Remembers the current position as the start position.
		/// </summary>
		/// <remarks>
		/// This is a required step if you want to call <see cref="GetSpan"/> later.
		/// </remarks>
		public void RememberStartPosition()
		{
			_StartIndex = _CurrentIndex;
		}

		/// <summary>
		/// Gets the character at the current position.
		/// </summary>
		/// <returns></returns>
		public char GetCurrentChar()
		{
			return _Text[_CurrentIndex];
		}

        /// <summary>
        /// Returns the text from the current position until the end.
        /// </summary>
        /// <returns></returns>
	    public string GetTextUntilEnd()
	    {
	        return _Text.Substring(_CurrentIndex);
	    }

        /// <inheritdoc />
	    public override string ToString()
	    {
	        return _Text;
	    }

	    /// <summary>
		/// Moves the current position to the next character.
		/// </summary>
		/// <returns></returns>
		public bool TryMoveToNextChar()
		{
			bool canMoveToNext = _CurrentIndex < _Text.Length - 1;
			if (canMoveToNext)
			{
				++_CurrentIndex;
			}
			else
			{
				EndReached = true;
			}
			return canMoveToNext;
		}

		/// <summary>
		/// Tries to move the current position to the next occurrence of the specified text.
		/// </summary>
		/// <param name="textToFind">The text to find.</param>
		/// <returns><c>true</c> if the text was found, <c>false</c> if not.</returns>
		public bool TryMoveTo(string textToFind)
		{
			int foundIndex = _Text.IndexOf(textToFind, _CurrentIndex, StringComparison.OrdinalIgnoreCase);
			if (foundIndex == -1)
				return false;
			_CurrentIndex = foundIndex;
			return true;
		}

		/// <summary>
		/// Tries to move the current position to the next occurrence of the specified character.
		/// </summary>
		/// <param name="charToFind">The character to find.</param>
		/// <param name="escapeChar">The escape character.</param>
		/// <returns>
		///   <c>true</c> if the character was found, <c>false</c> if not.
		/// </returns>
		public bool TryMoveTo(char charToFind, char escapeChar)
		{
			int foundIndex = -1;
			bool ignoreNextChar = false;
			for (int index = _CurrentIndex; index < _Text.Length; index++)
			{
				if (_Text[index] == escapeChar)
				{
					ignoreNextChar = true;
					continue;
				}
				if ((_Text[index] == charToFind) && !ignoreNextChar)
				{
					foundIndex = index;
					break;
				}
				ignoreNextChar = false;
			}
			if (foundIndex == -1)
				return false;
			_CurrentIndex = foundIndex;
			return true;
		}


		public void MoveToEnd()
		{
			_CurrentIndex = _Text.Length - 1;
			EndReached = true;
		}

		/// <summary>
		/// Gets a span that represents the text that starts with the character at the
		/// (remembered) start position and ends with the character before the
		/// current position - unless the end was reached, in which case the very
		/// last character is included.
		/// </summary>
		/// <returns></returns>
		public Span GetSpan()
		{
			return !EndReached
				? new Span(_Text, _StartIndex, _CurrentIndex - _StartIndex)
				: new Span(_Text, _StartIndex);
		}

		/// <summary>
		/// Determines whether the specified text is a match for the text that starts at the current position.
		/// </summary>
		/// <param name="text">The text to check for.</param>
		/// <returns>
		///   <c>true</c> if the specified text is a match; otherwise, <c>false</c>.
		/// </returns>
		public bool IsMatch(string text)
		{
			int availableLength = _Text.Length - _CurrentIndex;
			int length = Math.Min(availableLength, text.Length);
			return String.Equals(_Text.Substring(_CurrentIndex, length), text, StringComparison.Ordinal);
		}

		/// <summary>
		/// Determines whether the specified character matches the character at the current position.
		/// </summary>
		/// <param name="character">The character to check for.</param>
		/// <returns>
		///   <c>true</c> if the specified character matches; otherwise, <c>false</c>.
		/// </returns>
		public bool IsMatch(char character)
		{
			return GetCurrentChar() == character;
		}

		/// <summary>
		/// Determines whether one of the specified character matches the character at the current position.
		/// </summary>
		/// <param name="characters">The characters to check for.</param>
		/// <returns>
		///   <c>true</c> if the specified character matches; otherwise, <c>false</c>.
		/// </returns>
		public bool IsMatch(char[] characters)
		{
			if ((characters == null) || (characters.Length == 0))
				return false;
			char currentChar = GetCurrentChar();
			return characters.Any(character => character == currentChar);
		}

		/// <summary>
		/// Determines whether the character at the current position is a digit.
		/// </summary>
		/// <returns>
		///   <c>true</c> if the specified character is a digit; otherwise, <c>false</c>.
		/// </returns>
		public bool IsDigit()
		{
			return Char.IsDigit(GetCurrentChar());
		}

		/// <summary>
		/// Determines whether the character at the current position is a letter or a digit.
		/// </summary>
		/// <returns>
		///   <c>true</c> if the specified character is a letter or a digit; otherwise, <c>false</c>.
		/// </returns>
		public bool IsLetterOrDigit()
		{
			return Char.IsLetterOrDigit(GetCurrentChar());
		}

		/// <summary>
		/// Tries to advance the current position by the specified number of characters.
		/// If that is not possible, the current position will be set to the last character.
		/// </summary>
		/// <param name="count">The number of characters.</param>
		/// <returns><c>true</c> if succesful, <c>false</c> if the end of the text was reached</returns>
		public bool TryMoveBy(int count)
		{
			int desiredPosition = _CurrentIndex + count;
			int actualPosition = Math.Min(desiredPosition, _Text.Length - 1);
			_CurrentIndex = actualPosition;
			this.EndReached = actualPosition < desiredPosition;
			return !this.EndReached;
		}
	}
}
