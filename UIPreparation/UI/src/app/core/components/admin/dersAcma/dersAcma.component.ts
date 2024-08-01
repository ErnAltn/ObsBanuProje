import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { Mufredat } from '../mufredat/models/Mufredat';
import { MufredatService } from '../mufredat/services/Mufredat.service';
import { OgretimElemani } from '../ogretimElemani/models/OgretimElemani';
import { OgretimElemaniService } from '../ogretimElemani/services/OgretimElemani.service';
import { ST_AkademikDonem } from '../sT_AkademikDonem/models/ST_AkademikDonem';
import { ST_AkademikDonemService } from '../sT_AkademikDonem/services/ST_AkademikDonem.service';
import { ST_AkademikYil } from '../sT_AkademikYil/models/ST_AkademikYil';
import { ST_AkademikYilService } from '../sT_AkademikYil/services/ST_AkademikYil.service';
import { DersAcma } from './models/DersAcma';
import { DersAcmaService } from './services/DersAcma.service';

declare var jQuery: any;

@Component({
	selector: 'app-dersAcma',
	templateUrl: './dersAcma.component.html',
	styleUrls: ['./dersAcma.component.scss']
})
export class DersAcmaComponent implements AfterViewInit, OnInit {

	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id','akademikYilId', 'akademikDonemId', 'mufredatId', 'ogrElmId', 'kontenjan', 'createdDate', 'updatedDate', 'deletedDate',  'update', 'delete'];

	dersAcmaList: DersAcma[];
	dersAcma: DersAcma = new DersAcma();
	akademikYilList: ST_AkademikYil[];
	akademikDonemList: ST_AkademikDonem[];
	mufredatList: Mufredat[];
	ogrElmList: OgretimElemani[];
	dersAcmaAddForm: FormGroup;


	dersAcmaId: number;

	constructor(private dersAcmaService: DersAcmaService, private ogrElmService: OgretimElemaniService, private mufredatService: MufredatService, private akademikDonemService: ST_AkademikDonemService, private akademikYilService: ST_AkademikYilService, private lookupService: LookUpService, private alertifyService: AlertifyService, private formBuilder: FormBuilder, private authService: AuthService) { }

	ngAfterViewInit(): void {
		this.getDersAcmaList();
	}

	ngOnInit() {
		this.createDersAcmaAddForm();
		this.getAkademikYilList();
		this.getAkademikDonemList();
		this.getMufredatList();
		this.getOgrElmList();
	}


	getDersAcmaList() {
		this.dersAcmaService.getDersAcmaList().subscribe(data => {
			this.dersAcmaList = data;
			this.dataSource = new MatTableDataSource(data);
			this.configDataTable();
		});
	}

	getAkademikYilList() {
		this.akademikYilService.getST_AkademikYilList().subscribe(data => {
			this.akademikYilList = data
		})
	}

	getAkademikDonemList() {
		this.akademikDonemService.getST_AkademikDonemList().subscribe(data => {
			this.akademikDonemList = data
		})
	}

	getMufredatList() {
		this.mufredatService.getMufredatList().subscribe(data => {
			this.mufredatList = data
		})
	}

	getOgrElmList() {
		this.ogrElmService.getOgretimElemaniList().subscribe(data => {
			this.ogrElmList = data
		})
	}

	save() {

		if (this.dersAcmaAddForm.valid) {
			this.dersAcma = Object.assign({}, this.dersAcmaAddForm.value)

			if (this.dersAcma.id == 0)
				this.addDersAcma();
			else
				this.updateDersAcma();
		}

	}

	addDersAcma() {

		this.dersAcmaService.addDersAcma(this.dersAcma).subscribe(data => {
			this.getDersAcmaList();
			this.dersAcma = new DersAcma();
			jQuery('#dersacma').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.dersAcmaAddForm);

		})

	}

	updateDersAcma() {

		this.dersAcmaService.updateDersAcma(this.dersAcma).subscribe(data => {

			var index = this.dersAcmaList.findIndex(x => x.id == this.dersAcma.id);
			this.dersAcmaList[index] = this.dersAcma;
			this.dataSource = new MatTableDataSource(this.dersAcmaList);
			this.configDataTable();
			this.dersAcma = new DersAcma();
			jQuery('#dersacma').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.dersAcmaAddForm);

		})

	}

	createDersAcmaAddForm() {
		this.dersAcmaAddForm = this.formBuilder.group({
			id: [0],
			akademikYilId: [0, Validators.required],
			akademikDonemId: [0, Validators.required],
			mufredatId: [0, Validators.required],
			ogrElmId: [0, Validators.required],
			kontenjan: [0, Validators.required]
		})
	}

	deleteDersAcma(dersAcmaId: number) {
		this.dersAcmaService.deleteDersAcma(dersAcmaId).subscribe(data => {
			this.alertifyService.success(data.toString());
			this.dersAcmaList = this.dersAcmaList.filter(x => x.id != dersAcmaId);
			this.dataSource = new MatTableDataSource(this.dersAcmaList);
			this.configDataTable();
		})
	}

	getDersAcmaById(dersAcmaId: number) {
		this.clearFormGroup(this.dersAcmaAddForm);
		this.dersAcmaService.getDersAcmaById(dersAcmaId).subscribe(data => {
			this.dersAcma = data;
			this.dersAcmaAddForm.patchValue(data);
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
