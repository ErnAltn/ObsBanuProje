import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { ST_DerslikTuru } from './models/ST_DerslikTuru';
import { ST_DerslikTuruService } from './services/ST_DerslikTuru.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-sT_DerslikTuru',
	templateUrl: './sT_DerslikTuru.component.html',
	styleUrls: ['./sT_DerslikTuru.component.scss']
})
export class ST_DerslikTuruComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id','ad','ekstra', 'update','delete'];

	sT_DerslikTuruList:ST_DerslikTuru[];
	sT_DerslikTuru:ST_DerslikTuru=new ST_DerslikTuru();

	sT_DerslikTuruAddForm: FormGroup;


	sT_DerslikTuruId:number;

	constructor(private sT_DerslikTuruService:ST_DerslikTuruService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getST_DerslikTuruList();
    }

	ngOnInit() {

		this.createST_DerslikTuruAddForm();
	}


	getST_DerslikTuruList() {
		this.sT_DerslikTuruService.getST_DerslikTuruList().subscribe(data => {
			this.sT_DerslikTuruList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.sT_DerslikTuruAddForm.valid) {
			this.sT_DerslikTuru = Object.assign({}, this.sT_DerslikTuruAddForm.value)

			if (this.sT_DerslikTuru.id == 0)
				this.addST_DerslikTuru();
			else
				this.updateST_DerslikTuru();
		}

	}

	addST_DerslikTuru(){

		this.sT_DerslikTuruService.addST_DerslikTuru(this.sT_DerslikTuru).subscribe(data => {
			this.getST_DerslikTuruList();
			this.sT_DerslikTuru = new ST_DerslikTuru();
			jQuery('#st_derslikturu').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.sT_DerslikTuruAddForm);

		})

	}

	updateST_DerslikTuru(){

		this.sT_DerslikTuruService.updateST_DerslikTuru(this.sT_DerslikTuru).subscribe(data => {

			var index=this.sT_DerslikTuruList.findIndex(x=>x.id==this.sT_DerslikTuru.id);
			this.sT_DerslikTuruList[index]=this.sT_DerslikTuru;
			this.dataSource = new MatTableDataSource(this.sT_DerslikTuruList);
            this.configDataTable();
			this.sT_DerslikTuru = new ST_DerslikTuru();
			jQuery('#st_derslikturu').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.sT_DerslikTuruAddForm);

		})

	}

	createST_DerslikTuruAddForm() {
		this.sT_DerslikTuruAddForm = this.formBuilder.group({		
			id : [0],
ad : ["", Validators.required],
ekstra : ["", Validators.required]
		})
	}

	deleteST_DerslikTuru(sT_DerslikTuruId:number){
		this.sT_DerslikTuruService.deleteST_DerslikTuru(sT_DerslikTuruId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.sT_DerslikTuruList=this.sT_DerslikTuruList.filter(x=> x.id!=sT_DerslikTuruId);
			this.dataSource = new MatTableDataSource(this.sT_DerslikTuruList);
			this.configDataTable();
		})
	}

	getST_DerslikTuruById(sT_DerslikTuruId:number){
		this.clearFormGroup(this.sT_DerslikTuruAddForm);
		this.sT_DerslikTuruService.getST_DerslikTuruById(sT_DerslikTuruId).subscribe(data=>{
			this.sT_DerslikTuru=data;
			this.sT_DerslikTuruAddForm.patchValue(data);
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
