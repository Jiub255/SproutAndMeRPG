from PIL import Image
from pathlib import Path
import re
import glob

# For each directory that starts with 'Foozle':
height = 48
width = 528
for directory in glob.glob('*Foozle*'):
    image_dict = {} 

    # gather sets of 4 images by animation type into dictionary with last word of animation as key (numbers included)
    # gather all .png images
    image_paths = glob.glob('**/*.png', root_dir=directory, recursive=True)

    # Image.open() each image, put it in the dictionary(string, list of 4 images) based on the last word in the 
    #   filename. Add new entry if needed.
    character_name = ''
    for path in image_paths:
        filename = path.split('/')[-1]
        if 'spritesheet' in filename:
            continue
        if 'portrait' in filename:
            continue
        split_name = re.findall(r'[A-Z][^A-Z]*', filename)
        key = split_name[-1]
        character_name = split_name[0].lower()
        if key not in image_dict.keys():
            image_dict[key] = [Image.open(directory + '/' + path)]
        else:
            image_dict[key].append(Image.open(directory + '/' + path))
    

    # make new image, find height by total images * height of images, width is same as images
    spritesheet = Image.new('RGBA', (width, height * 4 * len(image_dict)))

    # iterate through the dictionary and paste each set of animations onto the big new image. Change y-offset each
    #   round. Alphabetically sort keys first for consistency.
    y_offset = 0
    for key in sorted(image_dict.keys()):
        for images in image_dict[key]:
     #       for image in images:
            spritesheet.paste(images, (0, y_offset))
            y_offset += height

    # save image in 'Foozle' directory as '[character name]_spritesheet.png'. Get character name from the first word
    #   from the animation names.
    spritesheet.save(directory + '/' + character_name + '_spritesheet.png')
