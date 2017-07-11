SourceToHtml
============

A configurable text processor that creates HTML from a (source code) text. Comes with configurations for
JavaScript, TypeScript, C# and JSON. Nothing fancy, but "good enough" for my personal purposes.

**Remark: The colorizing feature covers only the basics like comments, text literals and
keywords** - basically things that can be done without actually understanding the code.

* **SourceToHtml** contains the class `SourceToHtml` that does the work, intended to be used in other code.
* **SourceToHtml.Cmd** implements the simple command line tool **s2h.exe** that is also an exaxmple for using `SourceToHtml`.
* **SourceToHtml.Test** contains the unit tests.
