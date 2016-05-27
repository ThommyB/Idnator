# Idnator
Simple tool for generating id attribute for every element in android layout.

> This utility was written especially for testing department, where they needed to target element by ID, but problem was that MVVM based app, uses binding instead of ids.

## What it does
The application logic is very simple. It basicly iterates through every element in xml layout file and adds id to every element which doesn't have one. Id format is [ElementName][Increment], e.g. LinearLayout1.

<img src="https://github.com/ThommyB/Idnator/blob/master/code.PNG">
