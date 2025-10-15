import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ChoferService } from '../services/chofer.service';
import { ActivatedRoute, Router } from '@angular/router';
import { RouterModule } from '@angular/router';

@Component({
  standalone: true,
  imports: [RouterModule, CommonModule, ReactiveFormsModule],
  template: `
    <div class="form-container">
      <h3>{{editing ? 'Modificar' : 'Registrar Nuevo'}} Chofer</h3>
      <form [formGroup]="form" (ngSubmit)="save()">
        <div class="form-group">
          <label for="dni">DNI:</label>
          <input id="dni" formControlName="dni" placeholder="Ingrese el número de DNI" />
        </div>
        <div class="form-group">
          <label for="nombre">Nombre:</label>
          <input id="nombre" formControlName="nombre" placeholder="Ingrese el nombre" />
        </div>
        <div class="form-group">
          <label for="apellido">Apellido:</label>
          <input id="apellido" formControlName="apellido" placeholder="Ingrese el apellido" />
        </div>
        <div class="button-group">
          <button type="button" class="cancel-button" routerLink="/choferes">Cancelar</button>
          <button type="submit" class="save-button">Guardar Cambios</button>
        </div>
      </form>
    </div>
  `
})
export class ChoferFormComponent implements OnInit {
  form!: FormGroup;
  editing = false;

  constructor(private fb: FormBuilder, private svc: ChoferService, private route: ActivatedRoute, private router: Router) {}

  ngOnInit(): void {
    this.form = this.fb.group({ dni: [''], nombre: [''], apellido: [''] });
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.editing = true;
      this.svc.getById(id).subscribe({
        next: (x) => this.form.patchValue(x),
        error: (error) => {
          console.error('Error al cargar chofer:', error);
          alert(error);
          this.router.navigate(['/choferes']);
        }
      });
    }
  }

  save() {
    const value = this.form.value;
    if (this.editing) {
      this.svc.update(value.dni, value).subscribe({
        next: () => this.router.navigate(['/choferes']),
        error: (error) => {
          console.error('Error al actualizar chofer:', error);
          alert(error);
        }
      });
    } else {
      this.svc.create(value).subscribe({
        next: () => this.router.navigate(['/choferes']),
        error: (error) => {
          console.error('Error al crear chofer:', error);
          alert(error);
        }
      });
    }
  }
}
