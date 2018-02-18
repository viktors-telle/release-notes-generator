import { NgModule } from '@angular/core';
import { ServerModule } from '@angular/platform-server';
import { AppModuleShared } from './app.module.shared';
import { AppComponent } from './components/app/app';
import { getBaseUrl } from './app.module.browser';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { ExceptionInterceptor } from './common/exception-interceptor';
import { ProjectService } from './services/project-service';
import { RepositoryService } from './services/repository-service';
import { ReleaseNotesService } from './services/release-notes-service';

@NgModule({
    bootstrap: [ AppComponent ],
    imports: [
        ServerModule,
        AppModuleShared
    ],
    providers: [
        ProjectService,
        RepositoryService,
        ReleaseNotesService
   ]
})
export class AppModule {
}
