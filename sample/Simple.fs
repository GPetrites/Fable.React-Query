module Simple

open Feliz
open Fable.ReactQuery
open Fetch
open Browser.Dom
open Fable.Core.Extensions

type IRepoData =
    { name: string
      description: string
      subscribers_count: int
      stargazers_count: int
      forks_count: int }

let getRepoData () =
    promise {
        let! resp = fetch "https://api.github.com/repos/tannerlinsley/react-query" []
        return! resp.json<IRepoData> ()
    }

let getRepoDataAsync = getRepoData >> Async.AwaitPromise

[<ReactComponent>]
let Example () =
    let repoData = Query.useQuery ("repoData", getRepoData) []

    let repoData' = Query.useQuery ("repoData", getRepoDataAsync) []

    if repoData.isLoading then
        Html.div "Loading..."
    else
        let data = repoData.data

        Html.div [
            Html.h1 data.name
            Html.p data.description
            Html.strong (sprintf "👀 %i" data.subscribers_count)
            Html.strong (sprintf "✨ %i" data.stargazers_count)
            Html.strong (sprintf "🍴 %i" data.forks_count)
        ]

[<ReactComponent>]
let App () =
    let queryClient = QueryClient []
    QueryClientProvider queryClient [ Example() ]