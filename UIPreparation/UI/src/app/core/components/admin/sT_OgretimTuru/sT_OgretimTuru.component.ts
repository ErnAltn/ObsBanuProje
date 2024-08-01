import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { ST_OgretimTuru } from './models/ST_OgretimTuru';
import { ST_OgretimTuruService } from './services/ST_OgretimTuru.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-sT_OgretimTuru',
	templateUrl: './sT_OgretimTuru.component.html',
	styleUrls: ['./sT_OgretimTuru.component.scss']
})
export class ST_OgretimTuruComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id','ad','ekstra', 'update','delete'];

	sT_OgretimTuruList:ST_OgretimTuru[];
	sT_OgretimTuru:ST_OgretimTuru=new ST_OgretimTuru();

	sT_OgretimTuruAddForm: FormGroup;


	sT_OgretimTuruId:number;

	constructor(private sT_OgretimTuruService:ST_OgretimTuruService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getST_OgretimTuruList();
    }

	ngOnInit() {

		this.createST_OgretimTuruAddForm();
	}


	getST_OgretimTuruList() {
		this.sT_OgretimTuruService.getST_OgretimTuruList().subscribe(data => {
			this.sT_OgretimTuruList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.sT_OgretimTuruAddForm.valid) {
			this.sT_OgretimTuru = Object.assign({}, this.sT_OgretimTuruAddForm.value)

			if (this.sT_OgretimTuru.id == 0)
				this.addST_OgretimTuru();
			else
				this.updateST_OgretimTuru();
		}

	}

	addST_OgretimTuru(){

		this.sT_OgretimTuruService.addST_OgretimTuru(this.sT_OgretimTuru).subscribe(data => {
			this.getST_OgretimTuruList();
			this.sT_OgretimTuru = new ST_OgretimTuru();
			jQuery('#st_ogretimturu').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.sT_OgretimTuruAddForm);

		})

	}

	updateST_OgretimTuru(){

		this.sT_OgretimTuruService.updateST_OgretimTuru(this.sT_OgretimTuru).subscribe(data => {

			var index=this.sT_OgretimTuruList.findIndex(x=>x.id==this.sT_OgretimTuru.id);
			this.sT_OgretimTuruList[index]=this.sT_OgretimTuru;
			this.dataSource = new MatTableDataSource(this.sT_OgretimTuruList);
            this.configDataTable();
			this.sT_OgretimTuru = new ST_OgretimTuru();
			jQuery('#st_ogretimturu').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.sT_OgretimTuruAddForm);

		})

	}

	createST_OgretimTuruAddForm() {
		this.sT_OgretimTuruAddForm = this.formBuilder.group({		
			id : [0],
ad : ["", Validators.required],
ekstra : ["", Validators.required]
		})
	}

	deleteST_OgretimTuru(sT_OgretimTuruId:number){
		this.sT_OgretimTuruService.deleteST_OgretimTuru(sT_OgretimTuruId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.sT_OgretimTuruList=this.sT_OgretimTuruList.filter(x=> x.id!=sT_OgretimTuruId);
			this.dataSource = new MatTableDataSource(this.sT_OgretimTuruList);
			this.configDataTable();
		})
	}

	getST_OgretimTuruById(sT_OgretimTuruId:number){
		this.clearFormGroup(this.sT_OgretimTuruAddForm);
		this.sT_OgretimTuruService.getST_OgretimTuruById(sT_OgretimTuruId).subscribe(data=>{
			this.sT_OgretimTuru=data;
			this.sT_OgretimTuruAddForm.patchValue(data);
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

	checkClaim(claim:string):boolean{
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
