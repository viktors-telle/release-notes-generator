import { NgModule } from '@angular/core';
import { AppModuleShared } from './app.module.shared';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppComponent } from './components/app/app';
import { ExceptionInterceptor } from './common/exception-interceptor';
import { ProjectService } from './services/project-service';
import { RepositoryService } from './services/repository-service';

@NgModule({
    bootstrap: [ AppComponent ],
    imports: [
        BrowserModule,
        AppModuleShared
    ],
    providers: [
         { provide: 'BASE_URL', useFactory: getBaseUrl },
         ProjectService,
         RepositoryService,
         { provide: HTTP_INTERCEPTORS, useClass: ExceptionInterceptor, multi: true }
    ]
})
export class AppModule {
}

export function getBaseUrl() {
    return document.getElementsByTagName('base')[0].href;
}
