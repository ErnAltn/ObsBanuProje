import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { OgrenciService } from '../ogrenci/services/Ogrenci.service';
import { OgretimElemani } from '../ogretimElemani/models/OgretimElemani';
import { OgretimElemaniService } from '../ogretimElemani/services/OgretimElemani.service';
import { Danismanlik } from './models/Danismanlik';
import { DanismanlikService } from './services/Danismanlik.service';

declare var jQuery: any;

@Component({
	selector: 'app-danismanlik',
	templateUrl: './danismanlik.component.html',
	styleUrls: ['./danismanlik.component.scss']
})
export class DanismanlikComponent implements AfterViewInit, OnInit {

	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id','ogrElmID', 'ogrenciId', 'createdDate', 'updatedDate', 'deletedDate',  'update', 'delete'];

	danismanlikList: Danismanlik[];
	danismanlik: Danismanlik = new Danismanlik();
	ogretimElemaniList:OgretimElemani[];
	ogrenciList:OgretimElemani[];
	danismanlikAddForm: FormGroup;


	danismanlikId: number;

	constructor(private danismanlikService: DanismanlikService,private ogrenciService:OgrenciService,private ogretimElemanıService:OgretimElemaniService, private lookupService: LookUpService, private alertifyService: AlertifyService, private formBuilder: FormBuilder, private authService: AuthService) { }

	ngAfterViewInit(): void {
		this.getDanismanlikList();
	}

	ngOnInit() {

		this.createDanismanlikAddForm();
		this.getOgretimElemaniList();
		this.getOgrenciList();
	}


	getDanismanlikList() {
		this.danismanlikService.getDanismanlikList().subscribe(data => {
			this.danismanlikList = data;
			this.dataSource = new MatTableDataSource(data);
			this.configDataTable();
		});
	}

	getOgretimElemaniList(){
		this.ogretimElemanıService.getOgretimElemaniList().subscribe(data=>{
			this.ogretimElemaniList=data;
		})
	}

	getOgrenciList(){
		this.ogrenciService.getOgrenciList().subscribe(data=>{
			this.ogrenciList=data
		})
	}
	save() {

		if (this.danismanlikAddForm.valid) {
			this.danismanlik = Object.assign({}, this.danismanlikAddForm.value)

			if (this.danismanlik.id == 0)
				this.addDanismanlik();
			else
				this.updateDanismanlik();
		}

	}

	addDanismanlik() {

		this.danismanlikService.addDanismanlik(this.danismanlik).subscribe(data => {
			this.getDanismanlikList();
			this.danismanlik = new Danismanlik();
			jQuery('#danismanlik').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.danismanlikAddForm);

		})

	}

	updateDanismanlik() {

		this.danismanlikService.updateDanismanlik(this.danismanlik).subscribe(data => {

			var index = this.danismanlikList.findIndex(x => x.id == this.danismanlik.id);
			this.danismanlikList[index] = this.danismanlik;
			this.dataSource = new MatTableDataSource(this.danismanlikList);
			this.configDataTable();
			this.danismanlik = new Danismanlik();
			jQuery('#danismanlik').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.danismanlikAddForm);

		})

	}

	createDanismanlikAddForm() {
		this.danismanlikAddForm = this.formBuilder.group({
			id: [0],
			ogrElmID: [0, Validators.required],
			ogrenciId: [0, Validators.required]
		})
	}

	deleteDanismanlik(danismanlikId: number) {
		this.danismanlikService.deleteDanismanlik(danismanlikId).subscribe(data => {
			this.alertifyService.success(data.toString());
			this.danismanlikList = this.danismanlikList.filter(x => x.id != danismanlikId);
			this.dataSource = new MatTableDataSource(this.danismanlikList);
			this.configDataTable();
		})
	}

	getDanismanlikById(danismanlikId: number) {
		this.clearFormGroup(this.danismanlikAddForm);
		this.danismanlikService.getDanismanlikById(danismanlikId).subscribe(data => {
			this.danismanlik = data;
			this.danismanlikAddForm.patchValue(data);
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
