namespace JobTrack.Core

open System
open Microsoft.FSharp.Core

module Jobs =
    type public Salary = { Minimum: decimal; Maximum: decimal }

    type public Company =
        { Id: Guid
          Name: string
          Rating: float }

    type public Posting =
        { Id: Guid
          Title: string
          Company: Company
          PostDate: DateOnly
          Salary: Salary }


open Jobs

module Applications =
    type public Applicant = { Id: Guid; Name: string }

    type public Application =
        { Id: Guid
          Posting: Posting
          Applicant: Applicant
          DateApplied: DateOnly }

    type public Interview =
        { Id: Guid
          Date: DateOnly
          Went: bool
          Comments: string option }

    type public Offer = { Id: Guid }

    type public Response =
        | NoResponse
        | Rejection of Id: Guid * Date: DateOnly
        | InterviewInvite of Id: Guid * Date: DateOnly * Interview: Interview
        | JobOffer of Offer

module Persistence =
    let addOrUpdate addFun updateFun lookupFun posting : Posting =
        match lookupFun posting with
        | true -> updateFun posting
        | false -> addFun posting
