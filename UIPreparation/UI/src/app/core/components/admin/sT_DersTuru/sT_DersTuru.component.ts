import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { ST_DersTuru } from './models/ST_DersTuru';
import { ST_DersTuruService } from './services/ST_DersTuru.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-sT_DersTuru',
	templateUrl: './sT_DersTuru.component.html',
	styleUrls: ['./sT_DersTuru.component.scss']
})
export class ST_DersTuruComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id','ad','ekstra', 'update','delete'];

	sT_DersTuruList:ST_DersTuru[];
	sT_DersTuru:ST_DersTuru=new ST_DersTuru();

	sT_DersTuruAddForm: FormGroup;


	sT_DersTuruId:number;

	constructor(private sT_DersTuruService:ST_DersTuruService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getST_DersTuruList();
    }

	ngOnInit() {

		this.createST_DersTuruAddForm();
	}


	getST_DersTuruList() {
		this.sT_DersTuruService.getST_DersTuruList().subscribe(data => {
			this.sT_DersTuruList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.sT_DersTuruAddForm.valid) {
			this.sT_DersTuru = Object.assign({}, this.sT_DersTuruAddForm.value)

			if (this.sT_DersTuru.id == 0)
				this.addST_DersTuru();
			else
				this.updateST_DersTuru();
		}

	}

	addST_DersTuru(){

		this.sT_DersTuruService.addST_DersTuru(this.sT_DersTuru).subscribe(data => {
			this.getST_DersTuruList();
			this.sT_DersTuru = new ST_DersTuru();
			jQuery('#st_dersturu').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.sT_DersTuruAddForm);

		})

	}

	updateST_DersTuru(){

		this.sT_DersTuruService.updateST_DersTuru(this.sT_DersTuru).subscribe(data => {

			var index=this.sT_DersTuruList.findIndex(x=>x.id==this.sT_DersTuru.id);
			this.sT_DersTuruList[index]=this.sT_DersTuru;
			this.dataSource = new MatTableDataSource(this.sT_DersTuruList);
            this.configDataTable();
			this.sT_DersTuru = new ST_DersTuru();
			jQuery('#st_dersturu').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.sT_DersTuruAddForm);

		})

	}

	createST_DersTuruAddForm() {
		this.sT_DersTuruAddForm = this.formBuilder.group({		
			id : [0],
ad : ["", Validators.required],
ekstra : ["", Validators.required]
		})
	}

	deleteST_DersTuru(sT_DersTuruId:number){
		this.sT_DersTuruService.deleteST_DersTuru(sT_DersTuruId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.sT_DersTuruList=this.sT_DersTuruList.filter(x=> x.id!=sT_DersTuruId);
			this.dataSource = new MatTableDataSource(this.sT_DersTuruList);
			this.configDataTable();
		})
	}

	getST_DersTuruById(sT_DersTuruId:number){
		this.clearFormGroup(this.sT_DersTuruAddForm);
		this.sT_DersTuruService.getST_DersTuruById(sT_DersTuruId).subscribe(data=>{
			this.sT_DersTuru=data;
			this.sT_DersTuruAddForm.patchValue(data);
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
