module Simple
open Feliz
open Fable.ReactQuery
open Fetch
open Browser.Dom

type IRepoData = {
    name: string
    description: string
    subscribers_count: int
    stargazers_count: int
    forks_count: int
}

let getRepoData () =
    promise {
        let! resp = fetch "https://api.github.com/repos/tannerlinsley/react-query" []
        return! resp.json<IRepoData>()
    }

[<ReactComponent>]
let Example () =
    let query = useQuery "repoData" getRepoData []

    if query.isLoading then
        Html.div "Loading..."
    else
        let data = query.data
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
    QueryClientProvider queryClient [
        Example ()
    ]
