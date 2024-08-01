import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { ST_AkademikDonem } from './models/ST_AkademikDonem';
import { ST_AkademikDonemService } from './services/ST_AkademikDonem.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-sT_AkademikDonem',
	templateUrl: './sT_AkademikDonem.component.html',
	styleUrls: ['./sT_AkademikDonem.component.scss']
})
export class ST_AkademikDonemComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id','ad','ekstra', 'update','delete'];

	sT_AkademikDonemList:ST_AkademikDonem[];
	sT_AkademikDonem:ST_AkademikDonem=new ST_AkademikDonem();

	sT_AkademikDonemAddForm: FormGroup;


	sT_AkademikDonemId:number;

	constructor(private sT_AkademikDonemService:ST_AkademikDonemService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getST_AkademikDonemList();
    }

	ngOnInit() {

		this.createST_AkademikDonemAddForm();
	}


	getST_AkademikDonemList() {
		this.sT_AkademikDonemService.getST_AkademikDonemList().subscribe(data => {
			this.sT_AkademikDonemList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.sT_AkademikDonemAddForm.valid) {
			this.sT_AkademikDonem = Object.assign({}, this.sT_AkademikDonemAddForm.value)

			if (this.sT_AkademikDonem.id == 0)
				this.addST_AkademikDonem();
			else
				this.updateST_AkademikDonem();
		}

	}

	addST_AkademikDonem(){

		this.sT_AkademikDonemService.addST_AkademikDonem(this.sT_AkademikDonem).subscribe(data => {
			this.getST_AkademikDonemList();
			this.sT_AkademikDonem = new ST_AkademikDonem();
			jQuery('#st_akademikdonem').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.sT_AkademikDonemAddForm);

		})

	}

	updateST_AkademikDonem(){

		this.sT_AkademikDonemService.updateST_AkademikDonem(this.sT_AkademikDonem).subscribe(data => {

			var index=this.sT_AkademikDonemList.findIndex(x=>x.id==this.sT_AkademikDonem.id);
			this.sT_AkademikDonemList[index]=this.sT_AkademikDonem;
			this.dataSource = new MatTableDataSource(this.sT_AkademikDonemList);
            this.configDataTable();
			this.sT_AkademikDonem = new ST_AkademikDonem();
			jQuery('#st_akademikdonem').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.sT_AkademikDonemAddForm);

		})

	}

	createST_AkademikDonemAddForm() {
		this.sT_AkademikDonemAddForm = this.formBuilder.group({		
			id : [0],
ad : ["", Validators.required],
ekstra : ["", Validators.required]
		})
	}

	deleteST_AkademikDonem(sT_AkademikDonemId:number){
		this.sT_AkademikDonemService.deleteST_AkademikDonem(sT_AkademikDonemId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.sT_AkademikDonemList=this.sT_AkademikDonemList.filter(x=> x.id!=sT_AkademikDonemId);
			this.dataSource = new MatTableDataSource(this.sT_AkademikDonemList);
			this.configDataTable();
		})
	}

	getST_AkademikDonemById(sT_AkademikDonemId:number){
		this.clearFormGroup(this.sT_AkademikDonemAddForm);
		this.sT_AkademikDonemService.getST_AkademikDonemById(sT_AkademikDonemId).subscribe(data=>{
			this.sT_AkademikDonem=data;
			this.sT_AkademikDonemAddForm.patchValue(data);
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
