# CxZip in DotNet Core 6.0
## Description:
To create a smaller file for upload into the Checkmarx CxSAST Web Portal. 

When uploading a project for scanning, if the zip file is larger than 200 MB, you will not be able to upload it. To create a smaller zip file of only files with specified extensions supported by Checkmarx.

This version has been rewritten into .Net Core 6.0

Author: _Michael Fowler_
#### Supported Environments
* Windows
* Linux

## HOWTO: Usage
* Place the version for the appropiate OS in any directory
* Usage Syntax: `CxZip <src path> <dest path> [<whitelist path>]`
