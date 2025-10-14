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
    <h3>Crear/Editar Chofer</h3>
    <form [formGroup]="form" (ngSubmit)="save()">
      <label>DNI: <input formControlName="dni" /></label><br />
      <label>Nombre: <input formControlName="nombre" /></label><br />
      <label>Apellido: <input formControlName="apellido" /></label><br />
      <button type="submit">Guardar</button>
    </form>
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
      this.svc.getById(id).subscribe(x => this.form.patchValue(x));
    }
  }

  save() {
    const value = this.form.value;
    if (this.editing) {
      this.svc.update(value.dni, value).subscribe(() => this.router.navigate(['/choferes']));
    } else {
      this.svc.create(value).subscribe(() => this.router.navigate(['/choferes']));
    }
  }
}
