import { Component } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterModule, RouterOutlet],
  template: `
    <h1>Micros Escolares - Test tecnico</h1>
    <nav>
      <a routerLink="/">Home</a> |
      <a routerLink="/chicos">Chicos</a> |
      <a routerLink="/micros">Micros</a> |
      <a routerLink="/choferes">Choferes</a>
    </nav>
    <router-outlet></router-outlet>
  `
})
export class AppComponent {}
