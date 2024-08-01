import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { ST_DersSeviyesi } from './models/ST_DersSeviyesi';
import { ST_DersSeviyesiService } from './services/ST_DersSeviyesi.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-sT_DersSeviyesi',
	templateUrl: './sT_DersSeviyesi.component.html',
	styleUrls: ['./sT_DersSeviyesi.component.scss']
})
export class ST_DersSeviyesiComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id','ad','ekstra', 'update','delete'];

	sT_DersSeviyesiList:ST_DersSeviyesi[];
	sT_DersSeviyesi:ST_DersSeviyesi=new ST_DersSeviyesi();

	sT_DersSeviyesiAddForm: FormGroup;


	sT_DersSeviyesiId:number;

	constructor(private sT_DersSeviyesiService:ST_DersSeviyesiService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getST_DersSeviyesiList();
    }

	ngOnInit() {

		this.createST_DersSeviyesiAddForm();
	}


	getST_DersSeviyesiList() {
		this.sT_DersSeviyesiService.getST_DersSeviyesiList().subscribe(data => {
			this.sT_DersSeviyesiList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.sT_DersSeviyesiAddForm.valid) {
			this.sT_DersSeviyesi = Object.assign({}, this.sT_DersSeviyesiAddForm.value)

			if (this.sT_DersSeviyesi.id == 0)
				this.addST_DersSeviyesi();
			else
				this.updateST_DersSeviyesi();
		}

	}

	addST_DersSeviyesi(){

		this.sT_DersSeviyesiService.addST_DersSeviyesi(this.sT_DersSeviyesi).subscribe(data => {
			this.getST_DersSeviyesiList();
			this.sT_DersSeviyesi = new ST_DersSeviyesi();
			jQuery('#st_dersseviyesi').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.sT_DersSeviyesiAddForm);

		})

	}

	updateST_DersSeviyesi(){

		this.sT_DersSeviyesiService.updateST_DersSeviyesi(this.sT_DersSeviyesi).subscribe(data => {

			var index=this.sT_DersSeviyesiList.findIndex(x=>x.id==this.sT_DersSeviyesi.id);
			this.sT_DersSeviyesiList[index]=this.sT_DersSeviyesi;
			this.dataSource = new MatTableDataSource(this.sT_DersSeviyesiList);
            this.configDataTable();
			this.sT_DersSeviyesi = new ST_DersSeviyesi();
			jQuery('#st_dersseviyesi').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.sT_DersSeviyesiAddForm);

		})

	}

	createST_DersSeviyesiAddForm() {
		this.sT_DersSeviyesiAddForm = this.formBuilder.group({		
			id : [0],
ad : ["", Validators.required],
ekstra : ["", Validators.required]
		})
	}

	deleteST_DersSeviyesi(sT_DersSeviyesiId:number){
		this.sT_DersSeviyesiService.deleteST_DersSeviyesi(sT_DersSeviyesiId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.sT_DersSeviyesiList=this.sT_DersSeviyesiList.filter(x=> x.id!=sT_DersSeviyesiId);
			this.dataSource = new MatTableDataSource(this.sT_DersSeviyesiList);
			this.configDataTable();
		})
	}

	getST_DersSeviyesiById(sT_DersSeviyesiId:number){
		this.clearFormGroup(this.sT_DersSeviyesiAddForm);
		this.sT_DersSeviyesiService.getST_DersSeviyesiById(sT_DersSeviyesiId).subscribe(data=>{
			this.sT_DersSeviyesi=data;
			this.sT_DersSeviyesiAddForm.patchValue(data);
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
