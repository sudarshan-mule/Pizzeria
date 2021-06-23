export interface iOrder {
  food?: iFood,
  quantity?: number,
  size?: string,
  ingredients?: iIngredients[],
  toppings?: iToppings[],
  grandTotal?: number
}

export interface iFood {
  image?: string,
  name?: string,
  price?: number
}

export interface iIngredients {
  name?: string,
  isChecked?: boolean,
  price?: number
}

export interface iToppings {
  name?: string,
  isChecked?: boolean,
  price?: number
}
