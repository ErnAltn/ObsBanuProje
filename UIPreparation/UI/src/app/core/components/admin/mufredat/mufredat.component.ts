import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { Bolum } from '../bolum/models/Bolum';
import { BolumService } from '../bolum/services/Bolum.service';
import { DersHavuzu } from '../dersHavuzu/models/DersHavuzu';
import { DersHavuzuService } from '../dersHavuzu/services/DersHavuzu.service';
import { ST_AkademikDonem } from '../sT_AkademikDonem/models/ST_AkademikDonem';
import { ST_AkademikDonemService } from '../sT_AkademikDonem/services/ST_AkademikDonem.service';
import { ST_AkademikYil } from '../sT_AkademikYil/models/ST_AkademikYil';
import { ST_AkademikYilService } from '../sT_AkademikYil/services/ST_AkademikYil.service';
import { Mufredat } from './models/Mufredat';
import { MufredatService } from './services/Mufredat.service';

declare var jQuery: any;

@Component({
	selector: 'app-mufredat',
	templateUrl: './mufredat.component.html',
	styleUrls: ['./mufredat.component.scss']
})
export class MufredatComponent implements AfterViewInit, OnInit {

	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id', 'createdDate', 'updatedDate', 'deletedDate', 'bolumId', 'dersId', 'akedemikYilId', 'akedemikDonemId', 'dersDonemi', 'update', 'delete'];

	mufredatList: Mufredat[];
	bolumList: Bolum[];
	dersHavuzuList: DersHavuzu[];
	akademikYilList:ST_AkademikYil[];
	akademikDonemList:ST_AkademikDonem[];
	mufredat: Mufredat = new Mufredat();
	selectedDersHavuzuList:DersHavuzu[];
	isDersChange:boolean=false;

	mufredatAddForm: FormGroup;


	mufredatId: number;

	constructor(private mufredatService: MufredatService,private akademikDonemService:ST_AkademikDonemService ,private akademikYilService:ST_AkademikYilService, private dersHavuzuService: DersHavuzuService, private bolumService: BolumService, private lookupService: LookUpService, private alertifyService: AlertifyService, private formBuilder: FormBuilder, private authService: AuthService) { }

	ngAfterViewInit(): void {
		this.getMufredatList();
	}

	ngOnInit() {

		this.createMufredatAddForm();
		this.getBolumList();
		this.getDersList();
		this.getAkademikYilList();
		this.getAkademikDonemList();
	}


	getMufredatList() {
		this.mufredatService.getMufredatList().subscribe(data => {
			this.mufredatList = data;
			this.dataSource = new MatTableDataSource(data);
			this.configDataTable();
		});
	}

	getBolumList() {
		this.bolumService.getBolumList().subscribe(data => {
			this.bolumList = data
		})
	}
	getDersList() {
		this.dersHavuzuService.getDersHavuzuList().subscribe(data => {
			this.dersHavuzuList = data
		})
	}

	getAkademikYilList(){
		this.akademikYilService.getST_AkademikYilList().subscribe(data=>{
			this.akademikYilList=data
		})
	}

	getAkademikDonemList(){
		this.akademikDonemService.getST_AkademikDonemList().subscribe(data=>{
			this.akademikDonemList=data
		})
	}
	onItemSelect(comboType: string) {
		this.setComboStatus(comboType);
	  }
	
	  onSelectAll(comboType: string) {
		this.setComboStatus(comboType);
	  }
	  onItemDeSelect(comboType: string) {
		this.setComboStatus(comboType);
	  }
	
	  setComboStatus(comboType: string) {
		if (comboType == "Ders") this.isDersChange = true;
	  }
	  
	save() {

		if (this.mufredatAddForm.valid) {
			this.mufredat = Object.assign({}, this.mufredatAddForm.value)

			if (this.mufredat.id == 0)
				this.addMufredat();
			else
				this.updateMufredat();
		}

	}

	addMufredat() {

		this.mufredatService.addMufredat(this.mufredat).subscribe(data => {
			this.getMufredatList();
			this.mufredat = new Mufredat();
			jQuery('#mufredat').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.mufredatAddForm);

		})

	}

	updateMufredat() {

		this.mufredatService.updateMufredat(this.mufredat).subscribe(data => {

			var index = this.mufredatList.findIndex(x => x.id == this.mufredat.id);
			this.mufredatList[index] = this.mufredat;
			this.dataSource = new MatTableDataSource(this.mufredatList);
			this.configDataTable();
			this.mufredat = new Mufredat();
			jQuery('#mufredat').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.mufredatAddForm);

		})

	}

	createMufredatAddForm() {
		this.mufredatAddForm = this.formBuilder.group({
			id: [0],

			bolumId: [0, Validators.required],
			dersId: [0, Validators.required],
			akedemikYilId: [0, Validators.required],
			akedemikDonemId: [0, Validators.required],
			dersDonemi: [0, Validators.required]
		})
	}

	deleteMufredat(mufredatId: number) {
		this.mufredatService.deleteMufredat(mufredatId).subscribe(data => {
			this.alertifyService.success(data.toString());
			this.mufredatList = this.mufredatList.filter(x => x.id != mufredatId);
			this.dataSource = new MatTableDataSource(this.mufredatList);
			this.configDataTable();
		})
	}

	getMufredatById(mufredatId: number) {
		this.clearFormGroup(this.mufredatAddForm);
		this.mufredatService.getMufredatById(mufredatId).subscribe(data => {
			this.mufredat = data;
			this.mufredatAddForm.patchValue(data);
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
