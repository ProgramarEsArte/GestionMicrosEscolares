import { Component, OnInit } from '@angular/core';
import { ChoferService } from '../services/chofer.service';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  imports: [CommonModule, RouterLink],
  template: `
    <div class="page-container">
      <div class="header-container">
        <h3>Listado de Choferes</h3>
        <a routerLink="/choferes/new" class="create-link">Nuevo Chofer</a>
      </div>
      <div class="list-container">
        <ul class="list">
          <li *ngFor="let c of choferes" class="list-item">
            <div class="item-info">
              <span class="item-title">{{c.nombre}} {{c.apellido}}</span>
              <span class="item-detail">DNI: {{c.dni}}</span>
            </div>
            <div class="item-actions">
              <a [routerLink]="['/choferes', c.dni]" class="edit-link">Editar</a>
              <button (click)="deleteChofer(c.dni)" class="delete-button">Eliminar</button>
            </div>
          </li>
        </ul>
      </div>
    </div>
  `
})
export class ChoferListComponent implements OnInit {
  choferes: any[] = [];
  constructor(private svc: ChoferService) {}
  ngOnInit(): void {
    this.loadChoferes();
  }

  loadChoferes(): void {
    this.svc.getAll().subscribe({
      next: (x) => this.choferes = x,
      error: (error) => {
        console.error('Error al cargar choferes:', error);
        alert(error);
      }
    });
  }

  deleteChofer(dni: string): void {
    if (confirm('¿Está seguro que desea eliminar este chofer?')) {
      this.svc.delete(dni).subscribe({
        next: () => this.loadChoferes(),
        error: (error) => {
          console.error('Error al eliminar chofer:', error);
          alert(error);
        }
      });
    }
  }
}
