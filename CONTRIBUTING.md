# Contributing
This is a guide on how to contribute to the UnityUtils repository, and how to use it (Finding files and etc.)!

## Finding Issues to Work on
You can find the Todo list here: https://github.com/users/my-dimons/projects/2 <br><br>

To find issues to work on, look at what game version is currently in _progress_ and search by that game versions milestone (Ex. **v1.3.1 milestone**). Then from the list of tasks that have not been started, choose one that you could help with. <br><br>

If you want to create a feature request/bug report, create an issue and I may add it to the todo list.

## Where to find files
The UnityUtils scripts files can be found in `Unity-Utils\UnityUtils\Packages\UnityUtils\Runtime\ScriptUtils` <br>
Documentation files can be found in `Unity-Utils\docs\source` and most doc files are found in the `ScriptUtils` folder

## Docs
To figure out how to make the documentation, look at other documentation files (Hopefully most things are self explainatory, but this guide will be updated accordingly). <br><br> 

To generate the Doxygen files (For updating the documentation public members), open up the Unity project file in the terminal (`cd` into `UnityUtils`), then run `doxygen .\Doxyfile`. This command should output lots of text and generate lots of XML files.

If doxygen is not installed you will need to install it from: https://www.doxygen.nl/index.html

## Changing Versions 
To change the version edit the docs release and version in `Unity-Utils\docs\source\conf.py`, and then update the version in `Unity-Utils\UnityUtils\Packages\UnityUtils\package.json`

## Naming Branches
