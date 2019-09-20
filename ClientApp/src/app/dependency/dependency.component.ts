import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-dependency',
  templateUrl: './dependency.component.html',
  styleUrls: ['./dependency.component.scss']
})
export class DependencyComponent implements OnInit {
  title = "Dependency algorithm";
  i = 0;
  arrayObj = [];
  newlyConstructedArr;
  outputToUser = [];
  constructor() { }

  ngOnInit() {
  }

  submitData() {
    var letter = (<HTMLInputElement>document.getElementById('letterId')).value;
    var dependency = (<HTMLInputElement>document.getElementById('dependencyId')).value;
    this.arrayObj.push({letter, dependency});
    this.newlyConstructedArr = JSON.stringify(this.arrayObj);
    let paramsToReset: string[] = ["letterId", "dependencyId"];
    this.resetValues(paramsToReset);
  }

  resetValues(params: string[]) {
    params.forEach( (x) => (<HTMLInputElement>document.getElementById(x)).value = "");
  }

  calculate() {
    let arrayToTakeDependencies = [];
    let arrayToPlayWith = [];
    let newlyBakedArray = [];
    for (let item of this.arrayObj) {
      let letter = item.letter;
      let dependencies = item.dependency;

      if (dependencies) {//first if exists
        if (dependencies.indexOf(';') > -1) {// more than 1 dependency
          let splitDependencies = dependencies.split(';');
          splitDependencies.forEach((x) => arrayToTakeDependencies.push(x)); // it means that take all the dependencies
        } else {
          arrayToTakeDependencies.push(dependencies); // only 1 dependency on this letter;
        }
      }
      // loop through what is pushed and add new dependencies
      arrayToTakeDependencies.forEach((x) => arrayToPlayWith.push(this.helperFunctionReturnsDependenciesArray(x)));

      // merge two arrays into one nice distinct sorted array
      newlyBakedArray = [... new Set(arrayToTakeDependencies.concat(...arrayToPlayWith))].sort();
      let finalArray = newlyBakedArray;
      
      for (let item of newlyBakedArray) {
        const dependency = this.arrayObj.find( (z: any) => z.letter === item);
        if (dependency) {
          if (dependency.dependency.indexOf(';') > -1) {
            const valuesToPush = dependency.dependency.split(';');
            valuesToPush.forEach((v: any)=> finalArray.push(v));
          } else {
            finalArray.push(dependency.dependency);
            if (this.helperFunctionReturnsDependenciesArray(dependency.dependency) !== undefined) {
              finalArray.push(...this.helperFunctionReturnsDependenciesArray(dependency.dependency));
            }
          }
          finalArray = [...new Set(finalArray)].sort();
        } else {
          continue;
        }
      }

      this.outputToUser.push(`${letter} -> ${[...(new Set(finalArray))].sort()}`);
      arrayToTakeDependencies = [];
      arrayToPlayWith = [];
    }
  }

  helperFunctionReturnsDependenciesArray(letter: any) {
    const dependencyObj = this.arrayObj.find( (x) => x.letter === letter);
    if (dependencyObj) {
      if (dependencyObj.dependency.indexOf(';') > -1) {
        return dependencyObj.dependency.split(';');
      } else {
        return [...dependencyObj.dependency];
      }
    }
  }

}