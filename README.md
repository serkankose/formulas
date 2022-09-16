# formulas
This is UI around the financial formulas available in the .net library 'Financial-Formulas-Library-.NET-Standard'.

The original source code is at: https://github.com/srbrettle/Financial-Formulas-Library-.NET-Standard

No guarantess for the calculations
I didn't test all the formulas and possibly there are bugs.
The formulas in the library are loaded with reflection and executed with the given parameters. Default value is 1 at the moment for all parameters. 
For some formulas it can raise exception, the user should set realistic values.

    the UI
    It's written in .Net Blazor WebAssembly in an afternoon so it's not quite basic, it just works, there are lots of room for improvement, such as:
    <ul>
        <li>Better, more realistic sample parameters</li>
        <li>Range of values with realistic boundaries</li>
        <li>Using sliders for parameter values with sensible ranges like (0, 1], [0, 100], [0, 365] etc</li>
        <li>Charting with realistic random values</li>

    </ul>
