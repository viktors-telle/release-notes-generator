import { NgModule } from '@angular/core';
import { ServerModule } from '@angular/platform-server';
import { AppModuleShared } from './app.module.shared';
import { AppComponent } from './components/app/app';
import { getBaseUrl } from './app.module.browser';
import { ProjectService } from './services/projects-service';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { ExceptionInterceptor } from './common/exception-interceptor';

@NgModule({
    bootstrap: [ AppComponent ],
    imports: [
        ServerModule,
        AppModuleShared
    ],
    providers: [
        ProjectService
   ]
})
export class AppModule {
}
