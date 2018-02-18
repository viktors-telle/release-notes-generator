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
import { RepositoryEditComponent } from './components/repositories/repository-edit';
import { ReleaseNotesComponent } from './components/release-notes/release-notes';
import { FilterPipe } from './common/pipes/filter-pipe';
import { MatDialogModule, MatButtonModule, MatInputModule, MatDatepickerModule, MatNativeDateModule } from '@angular/material';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ReleaseNotesGeneratorComponent } from './components/release-notes/release-notes-generator';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        ProjectsComponent,
        ProjectEditComponent,
        RepositoriesComponent,
        RepositoryEditComponent,
        ReleaseNotesComponent,
        ReleaseNotesGeneratorComponent,
        FilterPipe
    ],
    entryComponents: [
        ReleaseNotesComponent,
        ReleaseNotesGeneratorComponent
    ],
    imports: [
        CommonModule,
        HttpClientModule,
        FormsModule,
        ReactiveFormsModule,
        MatDialogModule,
        BrowserAnimationsModule,
        MatButtonModule,
        MatInputModule,
        MatDatepickerModule,
        MatNativeDateModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'projects', component: ProjectsComponent },            
            { path: 'projects/:id', component: ProjectEditComponent},
            { path: 'repositories', component: RepositoriesComponent },
            { path: 'repositories/:id', component: RepositoryEditComponent},
            { path: '**', redirectTo: 'home' }
        ])
    ]
})
export class AppModuleShared {
}
