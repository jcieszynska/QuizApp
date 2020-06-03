import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class QuizService {

  //properties
  readonly rootUrl = 'https://localhost:44359'; //our api
  qns: any[]; //stores 10 rand questions
  seconds: number; //stores time taken
  timer; //counting time taken
  qnProgress: number; //saves nr of qs attended by participant
  correctAnswerCount: number = 0; //default nr of correct answers

  //
  constructor(private http: HttpClient) { }

  //display time
  displayTimeElapsed() {
    return Math.floor(this.seconds / 3600) + ':' + Math.floor(this.seconds / 60) + ':' + Math.floor(this.seconds % 60);
  }

  getParticipantName() {
    var participant = JSON.parse(localStorage.getItem('participant'));
    return participant.Name;
  }


  //http methods

  //insert participant
  insertParticipant(name: string, email: string) {
    var body = {
      Name: name,
      Email: email
    }
    return this.http.post(this.rootUrl + '/api/InsertParticipant', body); //post method
  }

  //getting questions
  
  getQuestions() {
    return this.http.get(this.rootUrl + '/api/Questions');
  }

  //getting answers
  getAnswers() {
    var body = this.qns.map(x => x.QnID);
    return this.http.post(this.rootUrl + '/api/Answers', body);
  }

  //send score&time to api
  submitScore() {
    var body = JSON.parse(localStorage.getItem('participant'));
    body.Score = this.correctAnswerCount;
    body.TimeSpent = this.seconds;
    return this.http.post(this.rootUrl + "/api/UpdateOutput", body);
  }


}
