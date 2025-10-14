import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { RouterModule } from '@angular/router';
import { routes } from './app.routes';
import { ChicosListComponent } from './chicos/chicos-list.component';
import { MicroListComponent } from './micro/micro-list.component';
import { ChoferListComponent } from './chofer/chofer-list.component';

@NgModule({
  declarations: [
    AppComponent,
    ChicosListComponent,
    MicroListComponent,
    ChoferListComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(routes),
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }