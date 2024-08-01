import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { DersHavuzu } from '../dersHavuzu/models/DersHavuzu';
import { DersHavuzuService } from '../dersHavuzu/services/DersHavuzu.service';
import { Ogrenci } from '../ogrenci/models/Ogrenci';
import { OgrenciService } from '../ogrenci/services/Ogrenci.service';
import { Sinav } from '../sinav/models/Sinav';
import { SinavService } from '../sinav/services/Sinav.service';
import { Degerlendirme } from './models/Degerlendirme';
import { DegerlendirmeService } from './services/Degerlendirme.service';

declare var jQuery: any;

@Component({
	selector: 'app-degerlendirme',
	templateUrl: './degerlendirme.component.html',
	styleUrls: ['./degerlendirme.component.scss']
})
export class DegerlendirmeComponent implements AfterViewInit, OnInit {

	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id','sinavId', 'ogrenciId', 'sinavNotu', 'createdDate', 'updatedDate', 'deletedDate',  'update', 'delete'];

	degerlendirmeList: Degerlendirme[];
	sinavList:Sinav[];
	ogrenciList:Ogrenci[];
	dersHavuzuList:DersHavuzu[];
	degerlendirme: Degerlendirme = new Degerlendirme();

	degerlendirmeAddForm: FormGroup;


	degerlendirmeId: number;

	constructor(private degerlendirmeService: DegerlendirmeService,private dersHavuzuService:DersHavuzuService,private ogrenciService:OgrenciService,private sinavService:SinavService, private lookupService: LookUpService, private alertifyService: AlertifyService, private formBuilder: FormBuilder, private authService: AuthService) { }

	ngAfterViewInit(): void {
		this.getDegerlendirmeList();
	}

	ngOnInit() {

		this.createDegerlendirmeAddForm();
		this.getSinavList();
		this.getOgrenciList();
	}


	getDegerlendirmeList() {
		this.degerlendirmeService.getDegerlendirmeList().subscribe(data => {
			this.degerlendirmeList = data;
			this.dataSource = new MatTableDataSource(data);
			this.configDataTable();
		});
	}

	getSinavList(){
		this.sinavService.getSinavList().subscribe(data=>{
			this.sinavList=data
		})
	}
	getOgrenciList(){
		this.ogrenciService.getOgrenciList().subscribe(data=>{
			this.ogrenciList=data
		})
	}
	getDersList(){
		this.dersHavuzuService.getDersHavuzuList().subscribe(data=>{
			this.dersHavuzuList=data
		});
	}
	save() {

		if (this.degerlendirmeAddForm.valid) {
			this.degerlendirme = Object.assign({}, this.degerlendirmeAddForm.value)

			if (this.degerlendirme.id == 0)
				this.addDegerlendirme();
			else
				this.updateDegerlendirme();
		}

	}

	addDegerlendirme() {

		this.degerlendirmeService.addDegerlendirme(this.degerlendirme).subscribe(data => {
			this.getDegerlendirmeList();
			this.degerlendirme = new Degerlendirme();
			jQuery('#degerlendirme').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.degerlendirmeAddForm);

		})

	}

	updateDegerlendirme() {

		this.degerlendirmeService.updateDegerlendirme(this.degerlendirme).subscribe(data => {

			var index = this.degerlendirmeList.findIndex(x => x.id == this.degerlendirme.id);
			this.degerlendirmeList[index] = this.degerlendirme;
			this.dataSource = new MatTableDataSource(this.degerlendirmeList);
			this.configDataTable();
			this.degerlendirme = new Degerlendirme();
			jQuery('#degerlendirme').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.degerlendirmeAddForm);

		})

	}

	createDegerlendirmeAddForm() {
		this.degerlendirmeAddForm = this.formBuilder.group({
			id: [0],

			sinavId: [0, Validators.required],
			ogrenciId: [0, Validators.required],
			sinavNotu: [0, Validators.required]
		})
	}

	deleteDegerlendirme(degerlendirmeId: number) {
		this.degerlendirmeService.deleteDegerlendirme(degerlendirmeId).subscribe(data => {
			this.alertifyService.success(data.toString());
			this.degerlendirmeList = this.degerlendirmeList.filter(x => x.id != degerlendirmeId);
			this.dataSource = new MatTableDataSource(this.degerlendirmeList);
			this.configDataTable();
		})
	}

	getDegerlendirmeById(degerlendirmeId: number) {
		this.clearFormGroup(this.degerlendirmeAddForm);
		this.degerlendirmeService.getDegerlendirmeById(degerlendirmeId).subscribe(data => {
			this.degerlendirme = data;
			this.degerlendirmeAddForm.patchValue(data);
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
