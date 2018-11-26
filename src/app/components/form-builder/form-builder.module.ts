import { NgModule } from '@angular/core';
import { FormBuilderComponent } from './form-builder.component';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
    { path: '', component: FormBuilderComponent }
];

@NgModule({
    declarations: [
        FormBuilderComponent
    ],
    imports: [
        CommonModule,
        FormsModule,
        RouterModule.forChild(routes)
    ]
})
export class FormBuilderModule {}
