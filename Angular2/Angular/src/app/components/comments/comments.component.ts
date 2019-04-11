import { Component, OnInit } from '@angular/core';
import { Input } from '@angular/core';
import { Comment } from 'src/app/models/comment';
import { CommentsService } from 'src/app/services/comments.service';

@Component({
  selector: 'app-comments',
  templateUrl: './comments.component.html',
  styleUrls: ['./comments.component.css']
})
export class CommentsComponent implements OnInit {

  @Input()
  public currentTaskId: number;

  public comments: Comment[];
  public newCommentDescription: string;

  constructor(private commentsService: CommentsService) { }

  ngOnInit() {
    
  }

  ngOnChanges(){
    this.commentsService.GetAllCommentsByTaskId(this.currentTaskId)
    .subscribe(
      (comments) => {
        this.comments = comments;
      }
    )
  }

  public createComment(description: string){
    this.commentsService.createComment(this.currentTaskId,description)
    .subscribe(
      (comment) => {
        this.commentsService.GetAllCommentsByTaskId(this.currentTaskId)
        .subscribe(
          (comments) => {
            this.comments = comments;
          }
        )
      }
    )
  }

}
