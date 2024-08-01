import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { ST_OgretimDili } from '../sT_OgretimDili/models/ST_OgretimDili';
import { ST_OgretimDiliService } from '../sT_OgretimDili/services/ST_OgretimDili.service';
import { ST_OgretimTuru } from '../sT_OgretimTuru/models/ST_OgretimTuru';
import { ST_OgretimTuruService } from '../sT_OgretimTuru/services/ST_OgretimTuru.service';
import { ST_ProgramTuru } from '../sT_ProgramTuru/models/ST_ProgramTuru';
import { ST_ProgramTuruService } from '../sT_ProgramTuru/services/ST_ProgramTuru.service';
import { Bolum } from './models/Bolum';
import { BolumService } from './services/Bolum.service';

declare var jQuery: any;

@Component({
	selector: 'app-bolum',
	templateUrl: './bolum.component.html',
	styleUrls: ['./bolum.component.scss']
})
export class BolumComponent implements AfterViewInit, OnInit {

	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id','bolumAdi', 'programTuruAdi', 'ogretimTuruAdi', 'ogretimDiliAdi', 'webAdresi', 'createdDate', 'updatedDate', 'deletedDate', 'update', 'delete'];

	bolumList: Bolum[];
	bolumDtoList: any;
	bolum: Bolum = new Bolum();
	programTuruList: ST_ProgramTuru[];
	ogretimDiliList: ST_OgretimDili[];
	ogretimTuruList: ST_OgretimTuru[];

	bolumAddForm: FormGroup;


	bolumId: number;

	constructor(private bolumService: BolumService, private programTuruService: ST_ProgramTuruService, private ogretimDiliService: ST_OgretimDiliService, private ogretimTuruService: ST_OgretimTuruService,
		 private lookupService: LookUpService, private alertifyService: AlertifyService, private formBuilder: FormBuilder, private authService: AuthService) { }

	ngAfterViewInit(): void {
		this.getBolumDtoList();
	}

	ngOnInit() {

		this.createBolumAddForm();
		this.getProgramTuru();
		this.getOgretimDili();
		this.getOgretimTuru();
	}


	getProgramTuru() {
		this.programTuruService.getST_ProgramTuruList().subscribe(data => {
			this.programTuruList = data
		});
	}

	getOgretimTuru(){
		this.ogretimTuruService.getST_OgretimTuruList().subscribe(data =>{
			this.ogretimTuruList = data
		})
	}

	getOgretimDili() {
		this.ogretimDiliService.getST_OgretimDiliList().subscribe(data => {
			this.ogretimDiliList = data
		});
	}



	// getBolumList() {
	// 	this.bolumService.getBolumList().subscribe(data => {
	// 		this.bolumList = data;
	// 		this.dataSource = new MatTableDataSource(data);
	// 		this.configDataTable();
	// 	});
	// }

	getBolumDtoList() {
		this.bolumService.getBolumDto().subscribe(data => {
			this.bolumDtoList = data;
			console.log(this.bolumDtoList);
			this.dataSource = new MatTableDataSource(this.bolumDtoList);
			this.configDataTable();
		});
	}
	save() {

		if (this.bolumAddForm.valid) {
			this.bolum = Object.assign({}, this.bolumAddForm.value)

			if (this.bolum.id == 0)
				this.addBolum();
			else
				this.updateBolum();
		}

	}

	addBolum() {

		this.bolumService.addBolum(this.bolum).subscribe(data => {
			this.getBolumDtoList();
			this.bolum = new Bolum();
			jQuery('#bolum').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.bolumAddForm);

		})

	}

	updateBolum() {

		this.bolumService.updateBolum(this.bolum).subscribe(data => {
		console.log(this.bolum);
			var index = this.bolumDtoList.findIndex(x => x.id == this.bolum.id);
			this.bolumDtoList[index] = this.bolum;
			console.log(this.bolumDtoList[index]);
			this.dataSource = new MatTableDataSource(this.bolumDtoList);
			this.configDataTable();
			this.bolum = new Bolum();
			jQuery('#bolum').modal('hide');
			this.getBolumDtoList();
			this.alertifyService.success(data);
			this.clearFormGroup(this.bolumAddForm);

		})

	}

	createBolumAddForm() {
		this.bolumAddForm = this.formBuilder.group({
			id: [0],
			programTuruId: [0, Validators.required],
			ogretimTuruId: [0, Validators.required],
			ogretimDiliId: [0, Validators.required],
			bolumAdi: ["", Validators.required],
			webAdresi: ["", Validators.required]
		})
	}

	deleteBolum(bolumId: number) {
		this.bolumService.deleteBolum(bolumId).subscribe(data => {
			this.alertifyService.success(data.toString());
			this.bolumList = this.bolumList.filter(x => x.id != bolumId);
			this.dataSource = new MatTableDataSource(this.bolumList);
			this.configDataTable();
		})
	}

	getBolumById(bolumId: number) {
		this.clearFormGroup(this.bolumAddForm);
		this.bolumService.getBolumById(bolumId).subscribe(data => {
			this.bolum = data;
			this.bolumAddForm.patchValue(data);
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
