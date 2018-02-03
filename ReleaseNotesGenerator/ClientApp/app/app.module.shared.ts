import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { ProjectsComponent } from './components/project/projects';
import { ProjectEditComponent } from './components/project/project-edit';
import { AppComponent } from './components/app/app';
import { NavMenuComponent } from './components/navmenu/navmenu';
import { HomeComponent } from './components/home/home';
import { RepositoriesComponent } from './components/repositories/repositories';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        ProjectsComponent,
        ProjectEditComponent,
        RepositoriesComponent
    ],
    imports: [
        CommonModule,
        HttpClientModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'projects', component: ProjectsComponent },
            { path: 'projects/:id', component: ProjectEditComponent},
            { path: 'repositories', component: RepositoriesComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ]
})
export class AppModuleShared {
}
