async function GetRecipes() {
    console.log("recipe");
    const response = await fetch("/api/Recipes", {
        method: "GET",
        headers: { "Accept": "application/json" }
    });

    if (response.ok === true) {
        const recipes = await response.json();
        let rows = ''
        let count = 1;
        recipes.forEach(recipe => {
            if (count % 2 !== 0) {
                rows += '<tr>'
                rows += row(recipe);

            }
            else {
                rows += row(recipe);
                rows += '</tr>'
            }
            count++;
        })

        $("tbody").html(rows);
    }
}


async function GetRecipe(id) {
    console.log("get!!!")
    const response = await fetch("/api/Recipes/" + id, {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        const recipe = await response.json();
        console.log('need recipe');
        console.log(recipe);


        let detailForm = `<form name="recipeForm " class="border recipe-form">
             <h3>${recipe.recipeName}</h3>
            <input type="hidden" name="id" value="0" />
            <ul class=" form-group col-md-5 ingredients">
                Ingredients:
            ${ getIngredient(recipe.ingredients)}
            </ul>
            <div class=" form-group col-md-5 processes">
                Processes:
            ${getProcess(recipe)}
            </div>
        </form>`
        $(".recipe-detail").html(detailForm);
    }
}

function getIngredient(ingredients) {
    console.log('ingredients');
    console.log(ingredients);
    let ingredientsStr = '';
    let process = '';
    ingredients.forEach(i => {
        ingredientsStr += `<li class="ingredient_item">
                ${i.ingredientName} - ${i.numbers}
            </li>`;
    });

    return ingredientsStr;

}

function getProcess(recipe) {
    let processesStr = '';
    recipe.processes.forEach(r => {
        processesStr += `<div class="process_item">
                ${r.description}
            </div>`
    });
    return processesStr;

}

function row(recipe) {
    return ` 
                <td class ="border recipe_table__cell">
                    <h4><a class="nav-link text-dark" asp-area="" onclick='GetRecipe(${recipe.recipeId})'">${recipe.recipeName}</a></h4>
                </td>
         `
}

function userRow(recipe, userId) {
    return ` 
                <td class ="border recipe_table__cell">
                    <h4><a class="nav-link text-dark" asp-area="" onclick='GetUserRecipe(${userId}, ${recipe.recipeId})'">${recipe.recipeName}</a></h4>
                </td>
         `
}


async function GetUserRecipes(userId) {
    console.log("recipe");
    const response = await fetch(`/api/${userId}/recipes`, {
        method: "GET",
        headers: { "Accept": "application/json" }
    });

    if (response.ok === true) {
        const user = await response.json();
        let rows = ''
        let count = 1;
        user.userRecipes.forEach(recipe => {
            if (count % 2 !== 0) {
                rows += '<tr>'
                rows += userRow(recipe, userId);

            }
            else {
                rows += userRow(recipe, userId);
                rows += '</tr>'
            }
            count++;
        })

        $("tbody").html(rows);
    }
}


async function GetUserRecipe(userId, recipeId) {
    console.log("get!!!")
    const response = await fetch(`/api/${userId}/recipe/${recipeId}`, {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        const recipe = await response.json();
        console.log('need recipe');
        console.log(recipe);


        let detailForm = `<form name="recipeForm " class="border recipe-form">
             <h3>${recipe.recipeName}</h3>
            <input type="hidden" name="id" value="0" />
            <ul class=" form-group col-md-5 ingredients">
                Ingredients:
            ${ getIngredient(recipe.ingredients)}
            </ul>
            <div class=" form-group col-md-5 processes">
                Processes:
            ${getProcess(recipe)}
            </div>
        </form>`
        $(".recipe-detail").html(detailForm);
    }
}

function Standart() {
    GetRecipes();    
}

function User(userId) {
    GetUserRecipes(userId);
}

Standart();