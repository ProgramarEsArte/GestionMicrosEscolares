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
    <h3>Crear/Editar Micro</h3>
    <form [formGroup]="form" (ngSubmit)="save()">
      <label>Patente: <input formControlName="patente" /></label><br />
      <label>ChoferDni: 
        <select formControlName="choferDni">
          <option [value]="''">-- Ninguno --</option>
          <option *ngFor="let ch of choferes" [value]="ch.dni">{{ch.nombre}} {{ch.apellido}}</option>
        </select>
      </label><br />

      <!--<label>Asignar Chicos (Ctrl+click):</label><br />
      <select multiple formControlName="chicos">
        <option *ngFor="let c of chicos" [value]="c.dni">{{c.nombre}} {{c.apellido}}</option>
      </select>-->

      <button type="submit">Guardar</button>
    </form>
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
    this.chicoSvc.getAll().subscribe(x => this.chicos = x);
    this.choferSvc.getAll().subscribe(x => this.choferes = x);

    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.editing = true;
      this.svc.getById(id).subscribe(x => this.form.patchValue({ patente: x.patente, choferDni: x.choferDni, chicos: x.chicos?.map((c: any) => c.dni) }));
    }
  }

  save() {
    const value = this.form.value;
    if (this.editing) {
      this.svc.update(value.patente, value).subscribe(() => this.router.navigate(['/micros']));
    } else {
      this.svc.create(value).subscribe(() => this.router.navigate(['/micros']));
    }
  }
}
