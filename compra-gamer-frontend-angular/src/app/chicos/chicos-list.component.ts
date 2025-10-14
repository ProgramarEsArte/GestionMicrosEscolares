import { Component, OnInit } from '@angular/core';
import { ChicoService } from '../services/chico.service';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  imports: [CommonModule, RouterLink],
  template: `
    <h3>Chicos</h3>
    <a routerLink="/chicos/new" class="create-link">Crear chico</a>
    <ul class="list">
      <li *ngFor="let c of chicos">
        {{c.dni}} - {{c.nombre}} {{c.apellido}} (Micro: {{c.microPatente}})
        <a [routerLink]="['/chicos', c.dni]" class="edit-link">Editar</a>
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
export class ChicosListComponent implements OnInit {
  chicos: any[] = [];
  constructor(private svc: ChicoService) {}
  ngOnInit(): void {
    this.svc.getAll().subscribe(x => this.chicos = x);
  }
}
