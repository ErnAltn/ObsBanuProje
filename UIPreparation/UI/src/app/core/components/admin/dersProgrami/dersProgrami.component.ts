import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { DersProgrami } from './models/DersProgrami';
import { DersProgramiService } from './services/DersProgrami.service';

declare var jQuery: any;

@Component({
	selector: 'app-dersProgrami',
	templateUrl: './dersProgrami.component.html',
	styleUrls: ['./dersProgrami.component.scss']
})
export class DersProgramiComponent implements AfterViewInit, OnInit {

	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id', 'createdDate', 'updatedDate', 'deletedDate', 'dersAcmaId', 'derslikId', 'dersGunuId', 'dersSaati', 'update', 'delete'];

	dersProgramiList: DersProgrami[];
	dersProgrami: DersProgrami = new DersProgrami();

	dersProgramiAddForm: FormGroup;


	dersProgramiId: number;

	constructor(private dersProgramiService: DersProgramiService, private lookupService: LookUpService, private alertifyService: AlertifyService, private formBuilder: FormBuilder, private authService: AuthService) { }

	ngAfterViewInit(): void {
		this.getDersProgramiList();
	}

	ngOnInit() {

		this.createDersProgramiAddForm();
	}


	getDersProgramiList() {
		this.dersProgramiService.getDersProgramiList().subscribe(data => {
			this.dersProgramiList = data;
			this.dataSource = new MatTableDataSource(data);
			this.configDataTable();
		});
	}

	save() {

		if (this.dersProgramiAddForm.valid) {
			this.dersProgrami = Object.assign({}, this.dersProgramiAddForm.value)

			if (this.dersProgrami.id == 0)
				this.addDersProgrami();
			else
				this.updateDersProgrami();
		}

	}

	addDersProgrami() {

		this.dersProgramiService.addDersProgrami(this.dersProgrami).subscribe(data => {
			this.getDersProgramiList();
			this.dersProgrami = new DersProgrami();
			jQuery('#dersprogrami').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.dersProgramiAddForm);

		})

	}

	updateDersProgrami() {

		this.dersProgramiService.updateDersProgrami(this.dersProgrami).subscribe(data => {

			var index = this.dersProgramiList.findIndex(x => x.id == this.dersProgrami.id);
			this.dersProgramiList[index] = this.dersProgrami;
			this.dataSource = new MatTableDataSource(this.dersProgramiList);
			this.configDataTable();
			this.dersProgrami = new DersProgrami();
			jQuery('#dersprogrami').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.dersProgramiAddForm);

		})

	}

	createDersProgramiAddForm() {
		this.dersProgramiAddForm = this.formBuilder.group({
			id: [0],
			dersAcmaId: [0, Validators.required],
			derslikId: [0, Validators.required],
			dersGunuId: [0, Validators.required],
			dersSaati: [null, Validators.required]
		})
	}

	deleteDersProgrami(dersProgramiId: number) {
		this.dersProgramiService.deleteDersProgrami(dersProgramiId).subscribe(data => {
			this.alertifyService.success(data.toString());
			this.dersProgramiList = this.dersProgramiList.filter(x => x.id != dersProgramiId);
			this.dataSource = new MatTableDataSource(this.dersProgramiList);
			this.configDataTable();
		})
	}

	getDersProgramiById(dersProgramiId: number) {
		this.clearFormGroup(this.dersProgramiAddForm);
		this.dersProgramiService.getDersProgramiById(dersProgramiId).subscribe(data => {
			this.dersProgrami = data;
			this.dersProgramiAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'id')
				group.get(key).setValue(0);
		});
	}

	checkClaim(claim: string): boolean {
		return this.authService.claimGuard(claim)
	}

	configDataTable(): void {
		this.dataSource.paginator = this.paginator;
		this.dataSource.sort = this.sort;
	}

	applyFilter(event: Event) {
		const filterValue = (event.target as HTMLInputElement).value;
		this.dataSource.filter = filterValue.trim().toLowerCase();

		if (this.dataSource.paginator) {
			this.dataSource.paginator.firstPage();
		}
	}

}
