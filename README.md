# Reddit User Analyser

Small, quick, and dirty Notebook for quickly analyzing someone's Reddit profile. 

What we're doing is basically retrieve as many comments and posts as we can, so that we can see what kind of person the user is. We can see where they post / comment, and a breakdown of how many posts vs comments they usually do. It's a good way of figuring out, if someone is worth your time on Reddit.

When running the program, we're generating a ```json``` file for the user, that contains all subreddits they've been active in (for the last 1000 comments and posts). Then we take the top 10 most active subreddits and we create a diagram out of it.

# Examples

I'll be providing 2 examples of running the program. The first will be me, the second a crazy Trump conspiracy theorist.

First off, here's part of the json that get's generated for me:

```json
{
  "Genshin_Impact": {
    "TotalPosts": 7,
    "TotalPostsKarma": 2346,
    "TotalComments": 208,
    "TotalCommentsKarma": 3065,
    "TotalActivity": 215,
    "TotalKarma": 5411
  },
  "Denmark": {
    "TotalPosts": 1,
    "TotalPostsKarma": 1,
    "TotalComments": 105,
    "TotalCommentsKarma": 1344,
    "TotalActivity": 106,
    "TotalKarma": 1345
  },
  "DestinyTheGame": {
    "TotalPosts": 1,
    "TotalPostsKarma": 5713,
    "TotalComments": 95,
    "TotalCommentsKarma": 976,
    "TotalActivity": 96,
    "TotalKarma": 6689
  }
}
```

All of this then gets parsed into a diagram, that looks like this:

![Diagram over the user u/IAmDrNoLife](./docs/IAmdrnolife.png)

Quite obvious where I'm most active, which is clearly Genshin, however I basically don't ever posts in there. I only really comment.

We can then compare that diagram with the one of a conspiracy theorist:

![Diagram over a Trumpist conspiracy theorist](./docs/Allan_QuartermainSr.png)

# How to use

You need to create a json file named ```settings.json``` which should contain a ```ClientId``` and a ```ClientSecret``` which should be your responsibility to get, from the Reddit API (maybe rip soon, due to the lunatic changes Reddit are doing right now...).

You can register a new app here: https://www.reddit.com/prefs/apps

Furthermore, you also need to setup the list of users to retrieve. Here's a code example of it:

```python
list_of_users = [
    ("I-melted", UserType.Random), 
    ("TheAvatar99", UserType.Chad),
    ("krnnz", UserType.Autism), 
    ("Its_Cmac", UserType.Autism), 
    ("WideEstablishment578", UserType.Boomer), 
    ("IAmdrnolife", UserType.Me),
    ("relxp", UserType.Autism),
    ("bad_apiarist", UserType.Autism),
    ("ricochetblue", UserType.Normal),
    ("zerowo_", UserType.Chad),
]
```

Just follow the same structure. An array of tuples, where the first element is the name of the user, and the last is the UserType.

ps.. I thought I had made it so the last item was an array of UserTypes... But I can see that for some reason has been reverted (before I switched voer to Git)... Soooe, idk. I might change that back again in the future, but eh, for now we've got this.