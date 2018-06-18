using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weigelt.SourceToHtml
{
    /// <summary>
    /// Helper functions for creating typical settings.
    /// </summary>
    public static class CreateSettings
    {
        /// <summary>
        /// Creates settings for JavaScript (including keywords).
        /// </summary>
        public static readonly SourceToHtmlSettings ForJavaScript = new SourceToHtmlSettings
        {
            IdentifierSpecialChars = new[] { '_', '$' }, // + $
            QuoteChars = new[] { '"', '\'', '`' }, // + backtick
            Keywords = new[]
            {
                "break",
                "case",
                "catch",
                "class",
                "const",
                "continue",
                "debugger",
                "default",
                "delete",
                "do",
                "else",
                "export",
                "extends",
                "finally",
                "for",
                "function",
                "if",
                "import",
                "in",
                "instanceof",
                "new",
                "return",
                "super",
                "switch",
                "this",
                "throw",
                "try",
                "typeof",
                "var",
                "void",
                "while",
                "with",
                "yield",

                "null",
                "true",
                "false"
            }
        };

        /// <summary>
        /// Creates settings for TypeScript (including keywords).
        /// </summary>
        public static readonly SourceToHtmlSettings ForTypeScript = new SourceToHtmlSettings
        {
            IdentifierSpecialChars = new[] { '_', '$' }, // + $
            QuoteChars = new[] { '"', '\'', '`' }, // + backtick
            Keywords = new[]
            {
                "break",
                "case",
                "catch",
                "class",
                "const",
                "continue",
                "debugger",
                "default",
                "delete",
                "do",
                "else",
                "enum",
                "export",
                "extends",
                "false",
                "finally",
                "for",
                "function",
                "if",
                "import",
                "in",
                "instanceof",
                "new",
                "null",
                "return",
                "super",
                "switch",
                "this",
                "throw",
                "true",
                "try",
                "typeof",
                "var",
                "void",
                "while",
                "with",
                "implements",
                "interface",
                "let",
                "package",
                "private",
                "protected",
                "public",
                "static",
                "yield",
                "any",
                "boolean",
                "number",
                "string",
                "symbol",
                "abstract",
                "as",
                "async",
                "await",
                "constructor",
                "declare",
                "from",
                "get",
                "is",
                "module",
                "namespace",
                "of",
                "require",
                "set",
                "type"
            }
        };

        /// <summary>
        /// Creates settings for JavaScript (including keywords).
        /// </summary>
        public static readonly SourceToHtmlSettings ForCSharp = new SourceToHtmlSettings
        {
            Keywords = new[]
            {
                "abstract",
                "as",
                "base",
                "bool",
                "break",
                "byte",
                "case",
                "catch",
                "char",
                "checked",
                "class",
                "const",
                "continue",
                "decimal",
                "default",
                "delegate",
                "do",
                "double",
                "else",
                "enum",
                "event",
                "explicit",
                "extern",
                "false",
                "finally",
                "fixed",
                "float",
                "for",
                "foreach",
                "goto",
                "if",
                "implicit",
                "in",
                "in",
                "int",
                "interface",
                "internal",
                "is",
                "lock",
                "long",
                "namespace",
                "new",
                "null",
                "object",
                "operator",
                "out",
                "out",
                "override",
                "params",
                "private",
                "protected",
                "public",
                "readonly",
                "ref",
                "return",
                "sbyte",
                "sealed",
                "short",
                "sizeof",
                "stackalloc",
                "static",
                "string",
                "struct",
                "switch",
                "this",
                "throw",
                "true",
                "try",
                "typeof",
                "uint",
                "ulong",
                "unchecked",
                "unsafe",
                "ushort",
                "using",
                "virtual",
                "void",
                "volatile",
                "while",
                "add",
                "alias",
                "ascending",
                "descending",
                "dynamic",
                "from",
                "get",
                "global",
                "group",
                "into",
                "join",
                "let",
                "orderby",
                "partial",
                "remove",
                "select",
                "set",
                "value",
                "var",
                "where",
                "yield"
            }
        };

        /// <summary>
        /// Creates settings for JavaScript (including different colors for parameter names and values).
        /// </summary>
        public static readonly SourceToHtmlSettings ForJson;

        /// <summary>
        /// Creates settings for plain text (i.e. without any color coding).
        /// Tab characters are replaced according to <see cref="SourceToHtmlSettings.TabSize"/>
        /// </summary>
        public static readonly SourceToHtmlSettings ForPlainText = new SourceToHtmlSettings { KeepPlainText = true };

        /// <summary>
        /// Initializes the <see cref="CreateSettings"/> class.
        /// </summary>
        static CreateSettings()
        {
            ForJson = new SourceToHtmlSettings();
            ForJson.CssClasses.FirstTextLiteral = "srcJsonPropertyName";
            ForJson.CssClasses.TextLiteral = "srcJsonPropertyValue";
            ForJson.TextLiteralResetChars = new[] { '\r', '\n', '{', '}' };

            ForTypeScript.NumberSeparators = new[] { '_' }; // separator in TypeScript 2.7+
        }
    }
}
