import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { ST_DersDili } from './models/ST_DersDili';
import { ST_DersDiliService } from './services/ST_DersDili.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-sT_DersDili',
	templateUrl: './sT_DersDili.component.html',
	styleUrls: ['./sT_DersDili.component.scss']
})
export class ST_DersDiliComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id','ad','ekstra', 'update','delete'];

	sT_DersDiliList:ST_DersDili[];
	sT_DersDili:ST_DersDili=new ST_DersDili();

	sT_DersDiliAddForm: FormGroup;


	sT_DersDiliId:number;

	constructor(private sT_DersDiliService:ST_DersDiliService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getST_DersDiliList();
    }

	ngOnInit() {

		this.createST_DersDiliAddForm();
	}


	getST_DersDiliList() {
		this.sT_DersDiliService.getST_DersDiliList().subscribe(data => {
			this.sT_DersDiliList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.sT_DersDiliAddForm.valid) {
			this.sT_DersDili = Object.assign({}, this.sT_DersDiliAddForm.value)

			if (this.sT_DersDili.id == 0)
				this.addST_DersDili();
			else
				this.updateST_DersDili();
		}

	}

	addST_DersDili(){

		this.sT_DersDiliService.addST_DersDili(this.sT_DersDili).subscribe(data => {
			this.getST_DersDiliList();
			this.sT_DersDili = new ST_DersDili();
			jQuery('#st_dersdili').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.sT_DersDiliAddForm);

		})

	}

	updateST_DersDili(){

		this.sT_DersDiliService.updateST_DersDili(this.sT_DersDili).subscribe(data => {

			var index=this.sT_DersDiliList.findIndex(x=>x.id==this.sT_DersDili.id);
			this.sT_DersDiliList[index]=this.sT_DersDili;
			this.dataSource = new MatTableDataSource(this.sT_DersDiliList);
            this.configDataTable();
			this.sT_DersDili = new ST_DersDili();
			jQuery('#st_dersdili').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.sT_DersDiliAddForm);

		})

	}

	createST_DersDiliAddForm() {
		this.sT_DersDiliAddForm = this.formBuilder.group({		
			id : [0],
ad : ["", Validators.required],
ekstra : ["", Validators.required]
		})
	}

	deleteST_DersDili(sT_DersDiliId:number){
		this.sT_DersDiliService.deleteST_DersDili(sT_DersDiliId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.sT_DersDiliList=this.sT_DersDiliList.filter(x=> x.id!=sT_DersDiliId);
			this.dataSource = new MatTableDataSource(this.sT_DersDiliList);
			this.configDataTable();
		})
	}

	getST_DersDiliById(sT_DersDiliId:number){
		this.clearFormGroup(this.sT_DersDiliAddForm);
		this.sT_DersDiliService.getST_DersDiliById(sT_DersDiliId).subscribe(data=>{
			this.sT_DersDili=data;
			this.sT_DersDiliAddForm.patchValue(data);
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
