# Contributing
This is a guide on how to contribute to the UnityUtils repository, and how to use it (Finding files and etc.)!

## Finding Issues to Work on
You can find the Todo list here: https://github.com/users/my-dimons/projects/2 <br>

To find issues to work on, look at what game version is currently in _progress_ and search by that game versions milestone (Ex. **v1.3.1 milestone**). Then from the list of tasks that have not been started, choose one that you could help with. <br>

If you want to create a feature request/bug report, create an issue and I may add it to the todo list.

## Where to find files
The UnityUtils scripts files can be found in `UnityUtils\UnityUtils-UnityProject\Packages\UnityUtils\Runtime\ScriptUtils` <br>
Documentation files can be found in `UnityUtils\UnityUtils-UnityProject\docs\source` and most doc files are found in the `ScriptUtils` folder <br>
You can find the changelog in `UnityUtils\UnityUtils-UnityProject\Packages\UnityUtils\CHANGELOG.md`

## Docs
To figure out how to make the documentation, look at other documentation files (Hopefully most things are self explainatory, but this guide will be updated accordingly). <br>

To generate the Doxygen files (For updating the documentation public members), open up the Unity project file in the terminal (`cd` into `UnityUtils`), then run `doxygen .\Doxyfile`. This command should output lots of text and generate lots of XML files.

If doxygen is not installed you will need to install it from: https://www.doxygen.nl/index.html

## Changing Versions 
To change the version edit the docs release and version in `UnityUtils\docs\source\conf.py`, and then update the version in `UnityUtils\UnityUtils-UnityProject\Packages\UnityUtils\package.json`

## Updating Changelog
The changelog has a section for each version, each version has an `Additions`, `Fixes`, and `Changes` section

## Naming Branches
- Updates:
`v{version}`
|
`v{version}-RELEASED`
- Update feature/change:
`v{version}-{feature}`
- Tests:
`v{version}-TEST-{feature}`

### Example:
`v1.3.1`
`v1.3.0-RELEASED`
`v1.3.1-fix-bug`
`v1.3.1-add-feature`
`v1.3.1-TEST-test-doc-theme`
