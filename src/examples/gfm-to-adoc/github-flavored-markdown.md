# Github Flavored Markdown

Github Flavored Markdown (GFMD) is based on [Markdown Syntax Guide](http://daringfireball.net/projects/markdown/syntax) with some overwriting as described at [Github Flavored Markdown](http://github.github.com/github-flavored-markdown/)

## Text Writing
It is easy to write in GFMD. Just write simply like text and use the below simple "tagging" to mark the text and you are good to go!  

To specify a paragraph, leave 2 spaces at the end of the line  
... like this

## Headings

```
# Sample H1
## Sample H2
### Sample H3
```

will produce
# Sample H1
## Sample H2
### Sample H3



## Horizontal Rules

Horizontal rule is created using `---` on a line by itself.

---

## Coding - Block

```
    ```ruby
    # The Greeter class
    class Greeter
      def initialize(name)
        @name = name.capitalize
      end
    
      def salute
        puts "Hello #{@name}!"
      end
    end
    
    # Create a new object
    g = Greeter.new("world")
    
    # Output "Hello World!"
    g.salute
    ```
```
 
will produce  

```ruby
# The Greeter class
class Greeter
  def initialize(name)
    @name = name.capitalize
  end

  def salute
    puts "Hello #{@name}!"
  end
end

# Create a new object
g = Greeter.new("world")

# Output "Hello World!"
g.salute
```

Note: You can specify the different syntax highlighting based on the coding language eg. ruby, sh (for shell), php, etc  
Note: You must leave a blank line before the `\`\`\``

## Coding - In-line
You can produce inline-code by using only one \` to enclose the code:

```
This is some code: `echo something`
```

will produce  

This is some code: `echo something`


## Text Formatting
**Bold Text** is done using `**Bold Text**`  
<b>Bold Text</b> is done using `<b>Bold Text</b>`  
<strong>Strong Text</strong> is done using `<strong>Strong Text</strong>`  
*Italic Text* is done using `*Italic Text*`  
__*Bold Italic Text*__ is done using `__*Bold Italic Text*__`  
~~Strike-through Text~~ is done using `~~Strike-through Text~~`  
<mark>Marked Text</mark> is done using `<mark>Marked Text</mark>`  
<sub>Subscript Text</sub> is done using `<sub>Subscript Text</sub>`  
<sup>Superscript Text</sup> is done using `<sup>Superscript Text</sup>`  


## Hyperlinks

GFMD will automatically detect URL and convert them to links like this http://www.futureworkz.com/
To specify a link on a text, do this:

```
This is [an example](http://example.com/ "Title") inline link.
[This link](http://example.net/) has no title attribute.
```

This will produce

This is [an example](http://example.com/ "Title") inline link.
[This link](http://example.net/) has no title attribute.


## Escape sequence
You can escape using \\ eg. \\\`

## Creating list

Adding a `-` will change it into a list:

```
- Item 1
   * Sub item 1  
     with break
   * Sub item 2
- Item 2
   1. Sub item 3
   1. Sub item 4
- Item 3
```

will produce

- Item 1
   * Sub item 1  
     with break
   * Sub item 2
- Item 2
   1. Sub item 3
   1. Sub item 4
- Item 3

```
1. Item 1
   * Sub item 1  
     with break
   * Sub item 2
1. Item 2
   1. Sub item 3
   1. Sub item 4
1. Item 3
```

will produce

1. Item 1
   * Sub item 1  
     with break
   * Sub item 2
1. Item 2
   1. Sub item 3
   1. Sub item 4
1. Item 3


## Quoting

You can create a quote using `>`:

```
> This is a quote
>
> 1. Item 1
>    * Sub item 1  
>      with break
> 1. Item 2
> 1. Item 3

```

will produce

> This is a quote
>
> 1. Item 1
>    * Sub item 1  
>      with break
> 1. Item 2
> 1. Item 3

## Table


```
| foo  | bar  | baz  |
| ---: | :--: | :--- |
| bim  | bam  | bum  |
| ding | dang | dong |
```

will produce

| foo  | bar  | baz  |
| ---: | :--: | :--- |
| bim  | bam  | bum  |
| ding | dang | dong |


## Adding Image

```
![Branching Concepts](http://git-scm.com/figures/18333fig0319-tn.png "Branching Map")
```

will produce

![Branching Concepts](http://git-scm.com/figures/18333fig0319-tn.png "Branching Map")