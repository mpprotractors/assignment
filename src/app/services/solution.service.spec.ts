import { TestBed } from '@angular/core/testing';
import { SolutionService } from './solution.service';
import { UserInputInterface } from '../interfaces/user-input.interface';

describe('SolutionService', () => {
    let solutionService: SolutionService;
    const userInput: UserInputInterface[] = [
        { key: 'A', dependencies: [ 'B', 'C' ] },
        { key: 'B', dependencies: [ 'C', 'E' ] },
        { key: 'C', dependencies: [ 'G' ] },
        { key: 'D', dependencies: [ 'A', 'F' ] },
        { key: 'E', dependencies: [ 'F' ] },
        { key: 'F', dependencies: [ 'H' ] }
    ];
    const solution: UserInputInterface[] = [
        { key: 'A', dependencies: [ 'B', 'C', 'E', 'F', 'G', 'H' ] },
        { key: 'B', dependencies: [ 'C', 'E', 'F', 'G', 'H' ] },
        { key: 'C', dependencies: [ 'G' ] },
        { key: 'D', dependencies: [ 'A', 'B', 'C', 'E', 'F', 'G', 'H' ] },
        { key: 'E', dependencies: [ 'F', 'H' ] },
        { key: 'F', dependencies: [ 'H' ] }
    ];

    beforeEach(() => {
        TestBed.configureTestingModule({
            providers: [
                SolutionService
            ]
        });

        solutionService = TestBed.get(SolutionService);
    });

    it('should create solution service', () => {
        expect(solutionService).toBeTruthy();
    });

    it('should solve correctly', () => {
        solutionService.set(userInput);
        for (let i = 0; i < userInput.length; i++) {
            expect(solutionService.solve(userInput[i].dependencies)).toEqual(solution[i].dependencies);
        }
    });
});
