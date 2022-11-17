module Basic

open Feliz
open Fetch
open Fable.ReactQuery
open Fable.Core.JS

type IPostHeader = { id: int; title: string }

type IPost =
    { id: int
      title: string
      body: string }

let getPosts () =
    promise {
        let! resp = fetch "https://jsonplaceholder.typicode.com/posts" []
        return! resp.json<IPostHeader array> ()
    }

let getPost (postId: int) =
    promise {
        let! resp = fetch $"https://jsonplaceholder.typicode.com/posts/{postId}" []
        return! resp.json<IPost> ()
    }

[<ReactComponent>]
let Posts (setPostId: int option -> unit) =
    let queryClient = Query.useQueryClient []
    let posts = Query.useQuery ("posts", getPosts) []

    let post = queryClient.getQueryData<IPost> ("post", 1)

    match post with
    | Some p -> console.log (p)
    | None -> console.log ("Not cached")

    Html.div [
        Html.h1 "posts"
        Html.div [
            match posts.status with
            | Loading -> Html.div "Loading..."
            | Error -> Html.div "Error"
            | Success ->
                React.fragment [
                    Html.div [
                        for post in posts.data ->
                            let cached = queryClient.getQueryData<IPost> ("post", post.id)

                            Html.p [
                                prop.key post.id
                                prop.children [
                                    Html.anchor [
                                        prop.href "#"
                                        prop.text post.title
                                        prop.onClick (fun _ -> setPostId (Some post.id))
                                        match cached with
                                        | Some _ ->
                                            prop.style [
                                                style.fontWeight.bold
                                                style.color.green
                                            ]
                                        | None -> prop.style []
                                    ]
                                ]
                            ]
                    ]
                    Html.div (
                        if posts.isFetching then
                            "Background updating"
                        else
                            ""
                    )
                ]
        ]
    ]

[<ReactComponent>]
let Post (postId: int, setPostId: int option -> unit) =
    let post = Query.useQuery (("post", postId), getPost) []

    Html.div [
        Html.div [
            Html.anchor [
                prop.href "#"
                prop.text "back"
                prop.onClick (fun _ -> setPostId (None))
            ]
        ]
        match post.status with
        | Loading -> Html.div "Loading"
        | Error -> Html.div $"Error: {post.error.message}"
        | Success ->
            React.fragment [
                Html.h1 post.data.title
                Html.div [ Html.p post.data.body ]
                Html.div (
                    if post.isFetching then
                        "Background Updating..."
                    else
                        ""
                )
            ]
    ]

[<ReactComponent>]
let Example () =
    let (postId, setPostId) = React.useState<Option<int>> (None)

    Html.div [
        Html.paragraph [
            Html.text
                "As you visit the posts below, you will notice them in a loading state
        the first time you load them. However, after you return to this list and
        click on any posts you have already visited again, you will see them
        load instantly and background refresh right before your eyes! "
            Html.strong
                "(You may need to throttle your network speed to simulate longer
            loading sequences)"
        ]
        match postId with
        | Some i -> Post(i, setPostId)
        | None -> Posts(setPostId)
    ]