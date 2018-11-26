import { Injectable } from '@angular/core';
import { UserInputInterface } from '../interfaces/user-input.interface';

@Injectable({
    providedIn: 'root'
})
export class SolutionService {
    private object: UserInputInterface[];

    public set(data: UserInputInterface[]): void {
        this.object = data;
    }

    public solve(dependencies: string[], prevResult?: string[]): string[] {
        if (!this.object || !dependencies) {
            return;
        }

        const tmpDependencies = [ ...dependencies ];

        if (prevResult) {
            for (let i = 0; i < tmpDependencies.length; i++) {
                const keyIndex = prevResult.indexOf(tmpDependencies[i]);
                if (keyIndex > -1) {
                    tmpDependencies.splice(keyIndex, 1);
                }
            }
        }

        let results = prevResult ? [ ...prevResult, ...tmpDependencies ] : tmpDependencies;

        for (let i = 0; i < tmpDependencies.length; i++) {
            const keyIndex = this.object.findIndex(row => tmpDependencies[i] === row.key);
            if (keyIndex > -1) {
                results = this.solve(this.object[keyIndex].dependencies, results);
            }
        }

        return results.sort();
    }
}
