import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MicroService } from '../services/micro.service';
import { ChicoService } from '../services/chico.service';
import { ChoferService } from '../services/chofer.service';
import { ActivatedRoute, Router } from '@angular/router';
import { RouterModule } from '@angular/router';

@Component({
  standalone: true,
  imports: [RouterModule, CommonModule, ReactiveFormsModule],
  template: `
    <div class="form-container">
      <h3>{{editing ? 'Modificar' : 'Registrar Nuevo'}} Micro</h3>
      <form [formGroup]="form" (ngSubmit)="save()">
        <div class="form-group">
          <label for="patente">Patente:</label>
          <input id="patente" formControlName="patente" placeholder="Ingrese el número de patente" />
        </div>
        <div class="form-group">
          <label for="choferDni">Conductor:</label>
          <select id="choferDni" formControlName="choferDni" class="select-input">
            <option [value]="''">-- Seleccione un conductor --</option>
            <option *ngFor="let ch of choferes" [value]="ch.dni">{{ch.nombre}} {{ch.apellido}}</option>
          </select>
        </div>
        <div class="button-group">
          <button type="button" class="cancel-button" routerLink="/micros">Cancelar</button>
          <button type="submit" class="save-button">Guardar Cambios</button>
        </div>
      </form>
    </div>
  `
})
export class MicroFormComponent implements OnInit {
  form!: FormGroup;
  chicos: any[] = [];
  choferes: any[] = [];
  editing = false;

  constructor(private fb: FormBuilder, private svc: MicroService, private chicoSvc: ChicoService, private choferSvc: ChoferService, private route: ActivatedRoute, private router: Router) {}

  ngOnInit(): void {
    this.form = this.fb.group({ patente: [''], choferDni: [''], chicos: [[]] });
    
    this.chicoSvc.getAll().subscribe({
      next: x => this.chicos = x,
      error: error => {
        console.error('Error al cargar alumnos:', error);
        alert(error);
      }
    });
    
    this.choferSvc.getAll().subscribe({
      next: x => this.choferes = x,
      error: error => {
        console.error('Error al cargar choferes:', error);
        alert(error);
      }
    });

    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.editing = true;
      this.svc.getById(id).subscribe({
        next: x => this.form.patchValue({ 
          patente: x.patente, 
          choferDni: x.choferDni, 
          chicos: x.chicos?.map((c: any) => c.dni) 
        }),
        error: error => {
          console.error('Error al cargar micro:', error);
          alert(error);
          this.router.navigate(['/micros']);
        }
      });
    }
  }

  save() {
    const value = this.form.value;
    if (this.editing) {
      this.svc.update(value.patente, value).subscribe({
        next: () => this.router.navigate(['/micros']),
        error: error => {
          console.error('Error al actualizar micro:', error);
          alert(error);
        }
      });
    } else {
      this.svc.create(value).subscribe({
        next: () => this.router.navigate(['/micros']),
        error: error => {
          console.error('Error al crear micro:', error);
          alert(error);
        }
      });
    }
  }
}
