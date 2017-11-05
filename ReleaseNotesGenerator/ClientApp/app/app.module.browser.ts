import { NgModule } from '@angular/core';
import { AppModuleShared } from './app.module.shared';
import { AppComponent } from './components/app/app.component';
import { BrowserModule } from '@angular/platform-browser';
import { ProjectService } from './services/projectservice';

@NgModule({
    bootstrap: [ AppComponent ],
    imports: [
        BrowserModule,
        AppModuleShared
    ],
    providers: [
         { provide: 'BASE_URL', useFactory: getBaseUrl },
         ProjectService
    ]
})
export class AppModule {
}

export function getBaseUrl() {
    return document.getElementsByTagName('base')[0].href;
}
