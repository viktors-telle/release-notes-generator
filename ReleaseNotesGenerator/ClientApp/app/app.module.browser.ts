import { NgModule } from '@angular/core';
import { AppModuleShared } from './app.module.shared';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppComponent } from './components/app/app';
import { ProjectService } from './services/projects-service';
import { ExceptionInterceptor } from './common/exception-interceptor';

@NgModule({
    bootstrap: [ AppComponent ],
    imports: [
        BrowserModule,
        AppModuleShared
    ],
    providers: [
         { provide: 'BASE_URL', useFactory: getBaseUrl },
         ProjectService,
         { provide: HTTP_INTERCEPTORS, useClass: ExceptionInterceptor, multi: true }
    ]
})
export class AppModule {
}

export function getBaseUrl() {
    return document.getElementsByTagName('base')[0].href;
}
