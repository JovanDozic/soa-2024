package model

import (
	"encoding/json"
	"io"
)

type Club struct {
	Id          int    `bson:"id,omitempty" json:"id"`
	Name        string `bson:"name,omitempty" json:"name"`
	Description string `bson:"description,omitempty" json:"description"`
	URL         string `bson:"url,omitempty" json:"url"`
	OwnerId     int    `bson:"ownerId,omitempty" json:"owner_id"`
}

type Clubs []*Club

func (p *Club) ToJSON(w io.Writer) error {
	e := json.NewEncoder(w)
	return e.Encode(p)
}

func (p *Club) FromJSON(r io.Reader) error {
	d := json.NewDecoder(r)
	return d.Decode(p)
}
