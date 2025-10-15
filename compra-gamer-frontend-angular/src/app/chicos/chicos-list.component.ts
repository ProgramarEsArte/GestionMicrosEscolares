import { Component, OnInit } from '@angular/core';
import { ChicoService } from '../services/chico.service';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  imports: [CommonModule, RouterLink],
  template: `
    <div class="page-container">
      <div class="header-container">
        <h3>Listado de Chicos</h3>
        <a routerLink="/chicos/new" class="create-link">Nuevo Chico</a>
      </div>
      <div class="list-container">
        <ul class="list">
          <li *ngFor="let c of chicos" class="list-item">
            <div class="item-info">
              <span class="item-title">{{c.nombre}} {{c.apellido}}</span>
              <span class="item-detail">DNI: {{c.dni}}</span>
              <span class="item-detail">Transporte: {{c.microPatente || 'No asignado'}}</span>
            </div>
            <div class="item-actions">
              <a [routerLink]="['/chicos', c.dni]" class="edit-link">Editar</a>
              <button (click)="deleteChico(c.dni)" class="delete-button">Eliminar</button>
            </div>
          </li>
        </ul>
      </div>
    </div>
  `
})
export class ChicosListComponent implements OnInit {
  chicos: any[] = [];
  constructor(private svc: ChicoService) {}
  ngOnInit(): void {
    this.loadChicos();
  }

  loadChicos(): void {
    this.svc.getAll().subscribe({
      next: (x) => this.chicos = x,
      error: (error) => {
        console.error('Error al cargar chicos:', error);
        alert(error);
      }
    });
  }

  deleteChico(dni: string): void {
    if (confirm('¿Está seguro que desea eliminar este chico?')) {
      this.svc.delete(dni).subscribe({
        next: () => this.loadChicos(),
        error: (error) => {
          console.error('Error al eliminar chico:', error);
          alert(error);
        }
      });
    }
  }
}
