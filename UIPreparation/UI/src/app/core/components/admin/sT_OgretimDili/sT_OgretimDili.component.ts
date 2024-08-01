import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { ST_OgretimDili } from './models/ST_OgretimDili';
import { ST_OgretimDiliService } from './services/ST_OgretimDili.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-sT_OgretimDili',
	templateUrl: './sT_OgretimDili.component.html',
	styleUrls: ['./sT_OgretimDili.component.scss']
})
export class ST_OgretimDiliComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id','ad','ekstra', 'update','delete'];

	sT_OgretimDiliList:ST_OgretimDili[];
	sT_OgretimDili:ST_OgretimDili=new ST_OgretimDili();

	sT_OgretimDiliAddForm: FormGroup;


	sT_OgretimDiliId:number;

	constructor(private sT_OgretimDiliService:ST_OgretimDiliService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getST_OgretimDiliList();
    }

	ngOnInit() {

		this.createST_OgretimDiliAddForm();
	}


	getST_OgretimDiliList() {
		this.sT_OgretimDiliService.getST_OgretimDiliList().subscribe(data => {
			this.sT_OgretimDiliList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.sT_OgretimDiliAddForm.valid) {
			this.sT_OgretimDili = Object.assign({}, this.sT_OgretimDiliAddForm.value)

			if (this.sT_OgretimDili.id == 0)
				this.addST_OgretimDili();
			else
				this.updateST_OgretimDili();
		}

	}

	addST_OgretimDili(){

		this.sT_OgretimDiliService.addST_OgretimDili(this.sT_OgretimDili).subscribe(data => {
			this.getST_OgretimDiliList();
			this.sT_OgretimDili = new ST_OgretimDili();
			jQuery('#st_ogretimdili').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.sT_OgretimDiliAddForm);

		})

	}

	updateST_OgretimDili(){

		this.sT_OgretimDiliService.updateST_OgretimDili(this.sT_OgretimDili).subscribe(data => {

			var index=this.sT_OgretimDiliList.findIndex(x=>x.id==this.sT_OgretimDili.id);
			this.sT_OgretimDiliList[index]=this.sT_OgretimDili;
			this.dataSource = new MatTableDataSource(this.sT_OgretimDiliList);
            this.configDataTable();
			this.sT_OgretimDili = new ST_OgretimDili();
			jQuery('#st_ogretimdili').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.sT_OgretimDiliAddForm);

		})

	}

	createST_OgretimDiliAddForm() {
		this.sT_OgretimDiliAddForm = this.formBuilder.group({		
			id : [0],
ad : ["", Validators.required],
ekstra : ["", Validators.required]
		})
	}

	deleteST_OgretimDili(sT_OgretimDiliId:number){
		this.sT_OgretimDiliService.deleteST_OgretimDili(sT_OgretimDiliId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.sT_OgretimDiliList=this.sT_OgretimDiliList.filter(x=> x.id!=sT_OgretimDiliId);
			this.dataSource = new MatTableDataSource(this.sT_OgretimDiliList);
			this.configDataTable();
		})
	}

	getST_OgretimDiliById(sT_OgretimDiliId:number){
		this.clearFormGroup(this.sT_OgretimDiliAddForm);
		this.sT_OgretimDiliService.getST_OgretimDiliById(sT_OgretimDiliId).subscribe(data=>{
			this.sT_OgretimDili=data;
			this.sT_OgretimDiliAddForm.patchValue(data);
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
