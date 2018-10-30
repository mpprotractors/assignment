## Overview

We ask you to implement dependency resolution algorithm. Transitive dependencies should be included as well. The following example dispays the transitive dependencies between letters of the alphabet. If A depends on B and B depends on C, then A also depends on C.

If this is input:

A -> BC  
B -> CE  
C -> G  
D -> AF  
E -> F  
F -> H  

Then this is output:

A -> BCEFGH  
B -> CEFGH  
C -> G  
D -> ABCEFGH  
E -> FH  
F -> H  

## TO-DO

* Create a web page that accepts user input
* Input can be processed either at the backend or in frontend. We leave it to your chose.
* Show results to user on a web page.
* You are free to use whichever frontend tooling you want.

## Criteria

* Code quality and structure - we appriciate such qualities of code as reliability reliability, maintainability and extensibility
* If you would be able to put in some unit tests - we would appreciate this as well
* Algorithm implementation

## HOW-TO

### Cloning repository and submitting

* Fork repository (instruction can be founde [here](https://help.github.com/articles/fork-a-repo/))
* Clone it to your computer (intructions [here](https://help.github.com/articles/cloning-a-repository/))
* After you complete your work we suggest you to create a pull request into original repository (instruction can be found [here](https://help.github.com/articles/creating-a-pull-request-from-a-fork/))


### Starting your work

* You can start developing now. You can do so either using [Visual Studio Code](https://code.visualstudio.com/) or Visual Studio ([Comunity](https://visualstudio.microsoft.com/vs/whatsnew/) version is available for free). You can use any other IDE but it will require you to configure .NET Core.
* From both VSC and VS you run and debug application by pressing F5
* MsBuild will automatically restore NuGet packages. But you will have to restore npm package manually before first run application by running ```npm install``` command in terminal in project folder
* Opening project in Visual Studio Code might require you to install C# extentions

### Project structure
In order to save you some time we had added some bootstrapping into project.

Entry point for JavaScript code is located in `Scripts/main.js`
All files from `Scripts` folder are being copies by `grunt` into `wwwroot/scripts` folder from where they are being served to clients. 

`index.html` is also located in `wwwroot` folder.

If you choose to implement assigniment using C# we recoment to do this using RESTfull services. You can create new controller in `Controllers` folder.

If you choose to implement it with JavaScript, fill free to place it into `Scripts` folder.



