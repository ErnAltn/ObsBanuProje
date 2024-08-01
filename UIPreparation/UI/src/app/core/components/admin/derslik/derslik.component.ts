import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { ST_DerslikTuru } from '../sT_DerslikTuru/models/ST_DerslikTuru';
import { ST_DerslikTuruService } from '../sT_DerslikTuru/services/ST_DerslikTuru.service';
import { Derslik } from './models/Derslik';
import { DerslikService } from './services/Derslik.service';

declare var jQuery: any;

@Component({
	selector: 'app-derslik',
	templateUrl: './derslik.component.html',
	styleUrls: ['./derslik.component.scss']
})
export class DerslikComponent implements AfterViewInit, OnInit {

	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id', 'createdDate', 'updatedDate', 'deletedDate', 'derslikTuruId', 'derslikAdi', 'kapasite', 'update', 'delete'];

	derslikList: Derslik[];
	derslikTuruList: ST_DerslikTuru[];
	derslik: Derslik = new Derslik();

	derslikAddForm: FormGroup;


	derslikId: number;

	constructor(private derslikService: DerslikService, private derslikTuruService: ST_DerslikTuruService, private lookupService: LookUpService, private alertifyService: AlertifyService, private formBuilder: FormBuilder, private authService: AuthService) { }

	ngAfterViewInit(): void {
		this.getDerslikList();
	}

	ngOnInit() {

		this.createDerslikAddForm();
		this.getDerslikTuru();
	}


	getDerslikList() {
		this.derslikService.getDerslikList().subscribe(data => {
			this.derslikList = data;
			this.dataSource = new MatTableDataSource(data);
			this.configDataTable();
		});
	}

	getDerslikTuru() {
		this.derslikTuruService.getST_DerslikTuruList().subscribe(data => {
			this.derslikTuruList = data
		});
	}

	save() {

		if (this.derslikAddForm.valid) {
			this.derslik = Object.assign({}, this.derslikAddForm.value)

			if (this.derslik.id == 0)
				this.addDerslik();
			else
				this.updateDerslik();
		}

	}

	addDerslik() {

		this.derslikService.addDerslik(this.derslik).subscribe(data => {
			this.getDerslikList();
			this.derslik = new Derslik();
			jQuery('#derslik').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.derslikAddForm);

		})

	}

	updateDerslik() {

		this.derslikService.updateDerslik(this.derslik).subscribe(data => {

			var index = this.derslikList.findIndex(x => x.id == this.derslik.id);
			this.derslikList[index] = this.derslik;
			this.dataSource = new MatTableDataSource(this.derslikList);
			this.configDataTable();
			this.derslik = new Derslik();
			jQuery('#derslik').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.derslikAddForm);

		})

	}

	createDerslikAddForm() {
		this.derslikAddForm = this.formBuilder.group({
			id: [0],
			derslikTuruId: [0, Validators.required],
			derslikAdi: ["", Validators.required],
			kapasite: [null, Validators.required]
		})
	}

	deleteDerslik(derslikId: number) {
		this.derslikService.deleteDerslik(derslikId).subscribe(data => {
			this.alertifyService.success(data.toString());
			this.derslikList = this.derslikList.filter(x => x.id != derslikId);
			this.dataSource = new MatTableDataSource(this.derslikList);
			this.configDataTable();
		})
	}

	getDerslikById(derslikId: number) {
		this.clearFormGroup(this.derslikAddForm);
		this.derslikService.getDerslikById(derslikId).subscribe(data => {
			this.derslik = data;
			this.derslikAddForm.patchValue(data);
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
