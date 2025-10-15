import { Component, OnInit } from '@angular/core';
import { MicroService } from '../services/micro.service';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  imports: [CommonModule, RouterLink],
  template: `
    <div class="page-container">
      <div class="header-container">
        <h3>Listado de Micros</h3>
        <a routerLink="/micros/new" class="create-link">Nuevo Micro</a>
      </div>
      <div class="list-container">
        <ul class="list">
          <li *ngFor="let m of micros" class="list-item">
            <div class="item-info">
              <span class="item-title">Patente: {{m.patente}}</span>
              <span class="item-detail">Conductor: {{m.choferDni || 'No asignado'}}</span>
              <span class="item-detail">Alumnos: {{m.cantidadChicos}}</span>
            </div>
            <div class="item-actions">
              <a [routerLink]="['/micros', m.patente]" class="edit-link">Editar</a>
              <button (click)="deleteMicro(m.patente)" class="delete-button">Eliminar</button>
            </div>
          </li>
        </ul>
      </div>
    </div>
  `
})
export class MicroListComponent implements OnInit {
  micros: any[] = [];
  constructor(private svc: MicroService) {}
  ngOnInit(): void {
    this.loadMicros();
  }

  loadMicros(): void {
    this.svc.getAll().subscribe({
      next: (x) => {
        console.log('Micros recibidos:', x);
        this.micros = x;
      },
      error: (error) => {
        console.error('Error al cargar micros:', error);
        alert(error);
      }
    });
  }

  deleteMicro(patente: string): void {
    if (confirm('¿Está seguro que desea eliminar este micro?')) {
      this.svc.delete(patente).subscribe({
        next: () => this.loadMicros(),
        error: (error) => {
          console.error('Error al eliminar micro:', error);
          alert(error);
        }
      });
    }
  }
}
