import { Routes } from '@angular/router';
import { ChicosListComponent } from './chicos/chicos-list.component';
import { MicroListComponent } from './micro/micro-list.component';
import { ChoferListComponent } from './chofer/chofer-list.component';
import { ChicosFormComponent } from './chicos/chicos-form.component';
import { MicroFormComponent } from './micro/micro-form.component';
import { ChoferFormComponent } from './chofer/chofer-form.component';
import { HomeComponent } from './home.component';

export const routes: Routes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: 'home', component: HomeComponent },
    { path: 'chicos', component: ChicosListComponent },
    { path: 'micros', component: MicroListComponent },
    { path: 'choferes', component: ChoferListComponent },
    { path: 'chicos/new', component: ChicosFormComponent },
    { path: 'chicos/:id', component: ChicosFormComponent },
    { path: 'micros/new', component: MicroFormComponent },
    { path: 'micros/:id', component: MicroFormComponent },
    { path: 'choferes/new', component: ChoferFormComponent },
    { path: 'choferes/:id', component: ChoferFormComponent }
];
