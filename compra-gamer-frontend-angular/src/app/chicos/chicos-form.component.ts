import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ChicoService } from '../services/chico.service';
import { ActivatedRoute, Router } from '@angular/router';
import { RouterModule } from '@angular/router';

@Component({
  standalone: true,
  imports: [RouterModule, CommonModule, ReactiveFormsModule],
  template: `
    <h3>Crear/Editar Chico</h3>
    <form [formGroup]="form" (ngSubmit)="save()">
      <label>DNI: <input formControlName="dni" /></label><br />
      <label>Nombre: <input formControlName="nombre" /></label><br />
      <label>Apellido: <input formControlName="apellido" /></label><br />
      <label>MicroPatente: <input formControlName="microPatente" /></label><br />
      <button type="submit">Guardar</button>
    </form>
  `
})
export class ChicosFormComponent implements OnInit {
  form!: FormGroup;
  editing = false;

  constructor(private fb: FormBuilder, private svc: ChicoService, private route: ActivatedRoute, private router: Router) {}

  ngOnInit(): void {
    this.form = this.fb.group({ dni: [''], nombre: [''], apellido: [''], microPatente: [''] });
    
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.editing = true;
      this.svc.getById(id).subscribe(chico => {
        this.form.patchValue(chico);
      });
    }
  }

  save() {
    const value = this.form.value;
    if (this.editing) {
      this.svc.update(value.dni, value).subscribe(() => this.router.navigate(['/chicos']));
    } else {
      this.svc.create(value).subscribe(() => this.router.navigate(['/chicos']));
    }
  }
}
