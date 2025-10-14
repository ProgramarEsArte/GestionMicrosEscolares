import { Component, OnInit } from '@angular/core';
import { MicroService } from '../services/micro.service';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  imports: [CommonModule, RouterLink],
  template: `
    <h3>Micros</h3>
    <a routerLink="/micros/new" class="create-link">Crear micro</a>
    <ul class="list">
      <li *ngFor="let m of micros">
        {{m.patente}} - Chofer: {{m.choferDni}} - Chicos: {{m.cantidadChicos}}
        <a [routerLink]="['/micros', m.patente]" class="edit-link">Editar</a>
      </li>
    </ul>
  `,
  styles: [`
    .list { list-style: none; padding: 0; }
    .list li { padding: 8px; border-bottom: 1px solid #eee; }
    .create-link { display: inline-block; margin-bottom: 1rem; padding: 8px 16px; background: #4CAF50; color: white; text-decoration: none; border-radius: 4px; }
    .edit-link { margin-left: 1rem; padding: 4px 8px; background: #2196F3; color: white; text-decoration: none; border-radius: 4px; font-size: 0.9em; }
  `]
})
export class MicroListComponent implements OnInit {
  micros: any[] = [];
  constructor(private svc: MicroService) {}
  ngOnInit(): void {
    this.svc.getAll().subscribe(x => {
      console.log('Micros recibidos:', x);
      this.micros = x;
    });
  }
}
