import { Component } from '@angular/core';
import { UserInputInterface } from '../../interfaces/user-input.interface';
import { SolutionService } from '../../services/solution.service';

@Component({
    selector: 'app-form-builder',
    templateUrl: './form-builder.component.html',
    styleUrls: ['./form-builder.component.scss']
})
export class FormBuilderComponent {
    public rows: UserInputInterface[];
    public results: UserInputInterface[];

    constructor(private solutionService: SolutionService) {
        this.rows = [];
        this.results = [];
        this.addRow();
    }

    public addRow(): void {
        this.rows.push({
            key: '',
            dependencies: ['']
        });
    }

    public addDependency(index: number): void {
        this.rows[index].dependencies.push('');
    }

    public submit(): void {
        this.results = [];
        this.solutionService.set(this.rows);
        for (let i = 0; i < this.rows.length; i++) {
            if (!this.rows[i].key) {
                continue;
            }

            this.results.push({
                key: this.rows[i].key,
                dependencies: this.solutionService.solve(this.rows[i].dependencies)
            });
        }
    }
}
